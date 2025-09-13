using System;
using Joyixir.GameManager.UI;
using Joyixir.GameManager.Utils;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsView : View
{
    public Button closeButton;
    public GameStatElement statElement;
    public Transform statParent;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(() =>
        {
            AnimateDown();
            GameManager.Instance.EnableController();
        });
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
    }
    public void Initialize(Common.Difficulty currentDifficulty, Common.Location currentLocation)
    {
        statElement.gameObject.SetActive(false);
        for (int i = 0; i < LevelConfigDatabase.Instance.configPairs.Count; i++)
        {
            var key = GetKey(i, currentLocation, currentDifficulty);
            var levelData = GMPrefs.GetLevelData($"{key}_{GMPrefs.ProfileName}");
            Common.LevelFinishData data = JsonUtility.FromJson<Common.LevelFinishData>(levelData);
            var statElementInstance = Instantiate(statElement, statParent);
            if (data != null)
            {
                statElementInstance.Initialize(data);
                statElementInstance.gameObject.SetActive(true);
            }
        }
    }
    
    
    private static string GetKey(int gameIndex, Common.Location location, Common.Difficulty difficulty)
    {
        return $"{location}_Game{gameIndex}_{difficulty}_Data";
    }
    
    protected override void OnBackBtn()
    {
        
    }
}
