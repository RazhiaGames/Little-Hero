using System.Collections.Generic;
using Joyixir.GameManager.Utils;
using UnityEngine;

public static class GameProgressManager
{
    private const int GamesPerZone = 8;

    private static List<Common.Location> locations = new List<Common.Location>
        { Common.Location.School, Common.Location.Hospital, Common.Location.AmusementPark };

    public static void MarkGamePlayed(int gameIndex, Common.Location location, 
        Common.Difficulty difficulty, Common.LevelFinishData finishData)
    {
        string key = GetKey(gameIndex, location, difficulty );
        GMPrefs.SetLevelData($"{key}_{GMPrefs.ProfileName}",JsonUtility.ToJson(finishData));
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    public static bool HasGameBeenPlayed(int gameIndex, Common.Location location, Common.Difficulty difficulty)
    {
        string key = GetKey(gameIndex, location, difficulty);
        Debug.Log(PlayerPrefs.HasKey(key));
        
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    private static string GetKey(int gameIndex, Common.Location location, Common.Difficulty difficulty)
    {
        return $"{location}_Game{gameIndex}_{difficulty}_{GMPrefs.ProfileName}";
    }

    public static bool AreAllGamesCompleted(Common.Location location, Common.Difficulty difficulty)
    {
        for (int i = 0; i < GamesPerZone; i++)
        {
            if (!HasGameBeenPlayed(i, location, difficulty))
                return false;
        }

        return true;
    }

    public static bool AreAllZonesCompleted(Common.Difficulty difficulty)
    {
        foreach (Common.Location location in System.Enum.GetValues(typeof(Common.Location)))
        {
            if (!AreAllGamesCompleted(location, difficulty))
                return false;
        }

        return true;
    }

    public static bool CanStartZone(Common.Location targetLocation, Common.Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Common.Difficulty.Easy:
                // Only School is available at first
                if (targetLocation != Common.Location.School &&
                    !AreAllGamesCompleted(Common.Location.School, Common.Difficulty.Easy))
                    return false;
                return true;

            case Common.Difficulty.Medium:
                if (!AreAllZonesCompleted(Common.Difficulty.Easy))
                    return false;
                return true;

            case Common.Difficulty.Hard:
                if (!AreAllZonesCompleted(Common.Difficulty.Medium))
                    return false;
                return true;

            default:
                return false;
        }
    }

    public static void SetZoneStarted(Common.Difficulty difficulty, Common.Location location)
    {
        PlayerPrefs.SetString($"StartedZone_{difficulty}_{GMPrefs.ProfileName}", location.ToString());
        PlayerPrefs.Save();
    }

    public static void DeleteZoneStarted(Common.Difficulty difficulty)
    {
        PlayerPrefs.DeleteKey($"StartedZone_{difficulty}_{GMPrefs.ProfileName}");
        PlayerPrefs.Save();
    }


    public static string GetZoneStartedName(Common.Difficulty difficulty)
    {
        return PlayerPrefs.GetString($"StartedZone_{difficulty}_{GMPrefs.ProfileName}");
    }

    public static bool IsDifferentZoneAlreadyStarted(Common.Difficulty difficulty, Common.Location currentLocation)
    {
        string key = $"StartedZone_{difficulty}_{GMPrefs.ProfileName}";
        if (!PlayerPrefs.HasKey(key)) return false;
        

        string startedZone = PlayerPrefs.GetString(key);
        return startedZone != currentLocation.ToString();
    }

    public static bool HasAllPrevDifficultiesPlayed(Common.Difficulty previousDifficulty)
    {
        foreach (Common.Location location in locations)
        {
            for (int i = 0; i < GamesPerZone; i++)
            {
                if (!HasGameBeenPlayed(i, location, previousDifficulty))
                    return false;
            }
        }

        return true;
    }
}