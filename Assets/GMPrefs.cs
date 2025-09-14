using UnityEngine;

namespace Joyixir.GameManager.Utils
{
    internal static class GMPrefs
    {
        public static bool IsMusic
        {
            get => PlayerPrefs.GetInt($"GM-IsMusic", 1) == 1;
            set => PlayerPrefs.SetInt($"GM-IsMusic", value ? 1 : 0);
        }

        public static void SetLevelData(string key, string levelData)
        {
            PlayerPrefs.SetString(key, levelData);
        }

        public static string GetLevelData(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public static void SetPlayerPositionAndRotation(Vector3 position, float rotation)
        {
            PlayerPrefs.SetFloat($"GM-PlayerPosX_{ProfileName}", position.x);
            PlayerPrefs.SetFloat($"GM-PlayerPosY_{ProfileName}", position.y);
            PlayerPrefs.SetFloat($"GM-PlayerPosZ_{ProfileName}", position.z);
            PlayerYRotation = rotation;
        }

        public static Vector3 GetPlayerPosition()
        {
            float x = PlayerPrefs.GetFloat($"GM-PlayerPosX_{ProfileName}", 0f);
            float y = PlayerPrefs.GetFloat($"GM-PlayerPosY_{ProfileName}", 0f);
            float z = PlayerPrefs.GetFloat($"GM-PlayerPosZ_{ProfileName}", 0f);
            return new Vector3(x, y, z);
        }
        
        public static float PlayerYRotation
        {
            get => PlayerPrefs.GetFloat($"GM-PlayerRotY_{ProfileName}", 0);
            set => PlayerPrefs.SetFloat($"GM-PlayerRotY_{ProfileName}", value);
        }
        
        public static int ProfilePicIndex
        {
            get => PlayerPrefs.GetInt($"GM-ProfileIndex_{ProfileName}", 0);
            set => PlayerPrefs.SetInt($"GM-ProfileIndex_{ProfileName}", value);
        }
        
        public static string ProfileName
        {
            get => PlayerPrefs.GetString($"GM-ProfileName", "");
            set => PlayerPrefs.SetString($"GM-ProfileName", value);
        }
        
        public static int StarCount
        {
            get => PlayerPrefs.GetInt($"GM-StarCount_{ProfileName}", 0);
            set => PlayerPrefs.SetInt($"GM-StarCount_{ProfileName}", value);
        }
    }
}