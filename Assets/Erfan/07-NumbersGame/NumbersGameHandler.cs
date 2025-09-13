using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumbersGameHandler : GameHandler
{
    public Transform spawnPoint;
    public int rightInBoxCount = 0;
    public int wrongInBoxCount = 0;
    public NumbersGameConfig.ZoneDifficultyConfig _zoneDConfig;
    public NumbersContainer containerPrefab;
    NumbersGameConfig currentConfig;
    public Transform containersParentTf;
    public float offsetAmount = -1f;
    
    private void Start()
    {
        containerPrefab.gameObject.SetActive(false);
        currentConfig = GameManager.Instance.currentLevelConfig as NumbersGameConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        zoneHowToPlayText = _zoneDConfig.howToPlayText;
        zoneHowToPlayAudio = _zoneDConfig.howToPlayAudio;

        float offsetRef;
        
        for (var i = 0; i < _zoneDConfig.containerTypes.Count; i++)
        {
            offsetRef = offsetAmount * i;
            
            var containerType = _zoneDConfig.containerTypes[i];
            var container = Instantiate(containerPrefab, containersParentTf);
            container.transform.localPosition = new Vector3(offsetRef, 0, 0); // LocalPosition for prefab children

            container.Initialize(containerType.containerText, containerType.containerType);
            if (container.itemType == Common.NumbersGameItemType.None)
                container.gameObject.SetActive(false);
            else
                container.gameObject.SetActive(true);
        }

        Instantiate(_zoneDConfig.numbersGamePrefab, spawnPoint);


        UIManager.Instance.HowToPlayAndInGameProcedure(_zoneDConfig.howToPlayText,_zoneDConfig.howToPlayAudio,
            () => { UpdateScore(); });
    }


    public void UpdateScore()
    {
        UIManager.Instance.inGameViewInstance.HideRightAndWrongContainer();
        UIManager.Instance.inGameViewInstance.AddToRights(rightInBoxCount);
        UIManager.Instance.inGameViewInstance.AddToWrongs(wrongInBoxCount);
        // if (rightInBoxCount >= _zoneDConfig.numToWin)
        // {
        //     DelayFinishGameBehaviour();
        // }
    }

    public async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var finishData = new Common.LevelFinishData(rightInBoxCount, 0,
            (int)Timer.Instance.timeRemaining, Common.GameWinState.Win, checkBtnCount, currentConfig.gameName);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }

    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;
        if (rightInBoxCount >= _zoneDConfig.numToWin && wrongInBoxCount <= 0)
        {
            gameState = Common.GameWinState.Win;
        }

        var finishData = new Common.LevelFinishData(rightInBoxCount, wrongInBoxCount,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount, currentConfig.gameName);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
        {
            GameManager.Instance.OnWinGame(finishData);
        }
    }

    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static NumbersGameHandler _instance;

    public static NumbersGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<NumbersGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(NumbersGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as NumbersGameHandler;
            if (isDontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    #endregion
}