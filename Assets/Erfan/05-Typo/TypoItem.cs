using System;
using System.Collections;
using RTLTMPro;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TypoItem : MonoBehaviour
{
    public RTLTextMeshPro text;
    public bool isWrong;
    public Button button;
    public UnityEvent<bool> onClickButton;
    [ShowIf("isWrong")]
    public string rightText;

    [ShowIf("isWrong")] public Image dotCoverImage;
    private Vector3 originalScale;
    private Coroutine wiggleCoroutine;
    private Color defaultTextColor;

    private void OnEnable()
    {
        button.onClick.AddListener(OnClickButton);
        originalScale = transform.localScale;
        defaultTextColor = text.color;
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClickButton);

        transform.localScale = originalScale;
        transform.localRotation = Quaternion.identity;
        if (wiggleCoroutine != null)
        {
            StopCoroutine(wiggleCoroutine);
            wiggleCoroutine = null;
        }
    }

    private async void OnClickButton()
    {
        onClickButton?.Invoke(isWrong);
        if (isWrong)
        {
            
            text.text = rightText;
            dotCoverImage.gameObject.SetActive(false);
            // var horizontalLayout = GetComponentInParent<HorizontalLayoutGroup>();
            // horizontalLayout.enabled = false;
            StaticTweeners.DoTextYoyoColor(text, Color.green);
            // await StaticTweeners.DoYoyoScale(text.transform);
            // horizontalLayout.enabled = true;

        }
        else
        {
            if (wiggleCoroutine != null)
            {
                StopCoroutine(wiggleCoroutine);
            }
            wiggleCoroutine = StartCoroutine(ScaleAndWiggleRed());
        }
        button.enabled = false;
    }


    private IEnumerator ScaleAndWiggleRed()
    {
        float duration = 0.3f;
        float wiggleAmount = 15f;
        float scaleUp = 1.15f;
        float elapsed = 0f;
        transform.localScale = originalScale * scaleUp;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float angle = Mathf.Sin(elapsed * 20f) * wiggleAmount;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            text.color = Color.Lerp(defaultTextColor, Color.red, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
        transform.localRotation = Quaternion.identity;
        text.color = defaultTextColor;
        wiggleCoroutine = null;
    }
    



}


