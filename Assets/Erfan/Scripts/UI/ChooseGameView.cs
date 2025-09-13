using System;
using System.Collections.Generic;
using Joyixir.GameManager.UI;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;


public class ChooseGameView : View
{
    public List<Button> Buttons = new List<Button>();
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public Button backButton;
    public Button closeButton;
    public Transform difficultyPanel;
    public Transform gamesPanel;
    public GameObject playLastDifficultyFirst;
    public RTLTextMeshPro zoneText;

    private void OnEnable()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            int sceneIndex = i; // Capture the index correctly in the closure
            var gameSelectionBtn = Buttons[i].GetComponent<GameSelectionButton>();
            Buttons[i].onClick.AddListener(() => 
                OnButtonClick(sceneIndex, LevelConfigDatabase.Instance.ConfigDictionary[gameSelectionBtn.gameType]));
        }


        easyButton.onClick.AddListener(() => OnClickDifficultyButton(Common.Difficulty.Easy));
        mediumButton.onClick.AddListener(() => OnClickDifficultyButton(Common.Difficulty.Medium));
        hardButton.onClick.AddListener(() => OnClickDifficultyButton(Common.Difficulty.Hard));
        backButton.onClick.AddListener(OnClickBackButton);
        closeButton.onClick.AddListener(OnClickCloseButton);
    }


    private void OnDisable()
    {
        // Remove all listeners to prevent duplicates or memory leaks
        foreach (var button in Buttons)
        {
            button.onClick.RemoveAllListeners();
        }

        easyButton.onClick.RemoveAllListeners();
        mediumButton.onClick.RemoveAllListeners();
        hardButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        zoneText.text = GameManager.Instance.currentLocationName;
    }

    private void OnClickBackButton()
    {
        playLastDifficultyFirst.gameObject.SetActive(false);
        gamesPanel.gameObject.SetActive(false);
        difficultyPanel.gameObject.SetActive(true);
    }

    private void OnClickCloseButton()
    {
        AnimateDown();
        GameManager.Instance.EnableController();
    }

public void OnClickDifficultyButton(Common.Difficulty difficulty)
{
    GameManager.Instance.currentDifficulty = difficulty;
    difficultyPanel.gameObject.SetActive(false);

    InitializeGames();
    gamesPanel.gameObject.SetActive(true);
}

public void OnButtonClick(int cardIndex, LevelConfig levelConfig)
{
    if (GameProgressManager.IsDifferentZoneAlreadyStarted(GameManager.Instance.currentDifficulty, GameManager.Instance.currentLocation))
    {
        var c = GameProgressManager.GetZoneStartedName(GameManager.Instance.currentDifficulty);
        var d = StaticUtils.ConvertZoneToFarsiName(c);
        UIManager.Instance.ShowText($"اول همه ی بازی های قبلی {d} رو تموم کن!");
        return;
    }
    var currentDifficulty = (int)GameManager.Instance.currentDifficulty;
    if (currentDifficulty - 1 >= 0)
    {
        var previousDifficulty = (Common.Difficulty)currentDifficulty - 1;
        // var isPrevDifficultyPlayed = GameProgressManager.HasGameBeenPlayed(cardIndex,
        //     GameManager.Instance.currentLocation,
        //     previousDifficulty);
        var isAllPrevDifficultiesPlayed = GameProgressManager.HasAllPrevDifficultiesPlayed(previousDifficulty);
        if (isAllPrevDifficultiesPlayed)
        {
            GameManager.Instance.OnGameCardClicked(+1, levelConfig);
        }

        else
        {
            playLastDifficultyFirst.gameObject.SetActive(true);
        }
    }

    else
    {
        GameManager.Instance.OnGameCardClicked(cardIndex+1, levelConfig);
    }
}

public void InitializeGames()
{
    for (var i = 0; i < Buttons.Count; i++)
    {
        var isPlayed = GameProgressManager.HasGameBeenPlayed(i, GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);

        if (!isPlayed)
        {
            Buttons[i].GetComponent<GameSelectionButton>().SetActive();
        }
        else
        {
            Buttons[i].GetComponent<GameSelectionButton>().SetInactive();
        }
    }
}




protected override void OnBackBtn()
{
}
}