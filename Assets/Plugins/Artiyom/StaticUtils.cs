using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using RTLTMPro;
using UnityEngine;


public static class StaticUtils
{
    private static readonly FastStringBuilder finalText = new FastStringBuilder(RTLSupport.DefaultBufferSize);

    public static string GetFixedRtlText(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        finalText.Clear();
        RTLSupport.FixRTL(input, finalText, true, true, true);
        // finalText.Reverse();

        return finalText.ToString();
    }


    public static string ConvertToFarsi(int input)
    {
        var tempString = input.ToString();
        return tempString.Replace('0', '\u06f0')
            .Replace('1', '\u06f1')
            .Replace('2', '\u06f2')
            .Replace('3', '\u06f3')
            .Replace('4', '\u06f4')
            .Replace('5', '\u06f5')
            .Replace('6', '\u06f6')
            .Replace('7', '\u06f7')
            .Replace('8', '\u06f8')
            .Replace('9', '\u06f9');
    }

    public static string GetClockUnitsFromSeconds(int timeInSeconds)
    {
        var timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        var hour = timeSpan.Hours;
        var minutes = timeSpan.Minutes;
        var seconds = timeSpan.Seconds;

        string tempStr = "";
        if (hour > 0)
        {
            var hourString = ConvertToFarsi(hour);
            tempStr = hourString + " ساعت ";
            if (minutes > 0)
            {
                var minString = ConvertToFarsi(minutes);
                tempStr += minString + " دقیقه ";
            }
        }

        if (hour <= 0)
        {
            var minString = ConvertToFarsi(minutes);
            tempStr += minString + " دقیقه ";
            if (seconds > 0)
            {
                var secondString = ConvertToFarsi(seconds);
                tempStr += secondString + " ثانیه ";
            }
        }

        if (minutes <= 0 && hour <= 0)
        {
            var secondString = ConvertToFarsi(seconds);
            tempStr = secondString + " ثانیه ";
        }

        return tempStr;
    }

    public static string GetRawMinAndSeconds(int timeInSeconds, bool isReverse = false)
    {
        var timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        var minutes = timeSpan.Minutes;
        var seconds = timeSpan.Seconds;

        string minString = "۰۰";
        string secondString = "";

        if (minutes > 0)
        {
            minString = ConvertToFarsi(minutes);
            if (minutes < 10)
                minString = "۰" + minString;
        }

        secondString = ConvertToFarsi(seconds);
        if (seconds < 10)
            secondString = "۰" + secondString;


        if (isReverse)
        {
            return secondString + ":" + minString;
        }

        return minString + ":" + secondString;
    }

    public static void ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject =
                    toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }

    public static TweenerCore<string, string, StringOptions> RTLTypeWriter(
        RTLTextMeshPro target,
        string endValue,
        float timePerCharacter = 0.05f,
        bool richTextEnabled = true,
        ScrambleMode scrambleMode = ScrambleMode.None,
        string scrambleChars = null)
    {
        target.DOKill();
        target.text = "";

        float duration = endValue.Length * timePerCharacter;

        TweenerCore<string, string, StringOptions> t = DOTween.To(() => target.text, x => target.text = x, endValue,
            duration);
        t.SetOptions(richTextEnabled, scrambleMode, scrambleChars)
            .SetTarget(target);

        return t;
    }

    public static bool HasComponent<T>(GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() != null;
    }


    public static Vector3 GetRandomVector3(Vector3 a, Vector3 b)
    {
        var x = UnityEngine.Random.Range(a.x, b.x);
        var y = UnityEngine.Random.Range(a.y, b.y);
        var z = UnityEngine.Random.Range(a.z, b.z);

        return new Vector3(x, y, z);
    }

    public static void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random(); // You can seed with a fixed value for predictable results
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            // Swap
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
    
    public static string ConvertZoneToFarsiName(string zoneName)
    {
        if (zoneName == "School")
        {
            return "مدرسه";
        }
        else if (zoneName == "Hospital")
        {
            return "بیمارستان";
        }
        else if (zoneName == "AmusementPark")
        {
            return "شهربازی";
        }
        return "";
    }

    #region UI

    public static bool AreUIRectsOverlappingFast(RectTransform rect1, RectTransform rect2, float scaleFactor = 1,
        float secondScaleFactor = 1)
    {
        Rect rect1World = GetWorldRect(rect1);
        Rect rect2World = GetWorldRect(rect2);

        rect1World = ScaleRect(rect1World, scaleFactor);
        rect2World = ScaleRect(rect2World, secondScaleFactor);

        return rect1World.Overlaps(rect2World, true);
    }

    private static Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners); // bottom-left, top-left, top-right, bottom-right

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];

        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;

        return new Rect(bottomLeft, new Vector2(width, height));
    }

    private static Rect ScaleRect(Rect rect, float scaleFactor)
    {
        // Calculate new size
        float newWidth = rect.width * scaleFactor;
        float newHeight = rect.height * scaleFactor;

        // Adjust position to keep center
        float x = rect.x - (newWidth - rect.width) * 0.5f;
        float y = rect.y - (newHeight - rect.height) * 0.5f;

        return new Rect(x, y, newWidth, newHeight);
    }

    #endregion
}