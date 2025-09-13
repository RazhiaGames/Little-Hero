using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;


public class FindPathGameHandler : GameHandler
{
    public Transform levelPrefabTf;

    public int rightScore;
    public int wrongScore;
    public int overallCounter;
    private List<Butterfly> _butterflyList = new List<Butterfly>();
    private FindPathConfig.ZoneDifficultyConfig _zoneDConfig;
    private FindPathConfig currentConfig;
    private int flowerCount = 0;
    private void Start()
    {
        currentConfig = GameManager.Instance.currentLevelConfig as FindPathConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        zoneHowToPlayText = _zoneDConfig.howToPlayText;
        zoneHowToPlayAudio = _zoneDConfig.howToPlayAudio;


        UIManager.Instance.HowToPlayAndInGameProcedure(_zoneDConfig.howToPlayText, _zoneDConfig.howToPlayAudio);
        var level = Instantiate(_zoneDConfig.FindPathLevel, levelPrefabTf);
        flowerCount = level.flowers.Count;
        Debug.Log(flowerCount);
        for (var i = 0; i < level.splines.Count; i++)
        {
            var spline = level.splines[i];
            var firstKnot = spline[^1];
            Vector3 localPos = firstKnot.Position;

            // Convert local spline point to world space
            Vector3 worldPos = level.m_Spline.transform.TransformPoint(localPos);
            // var randomZ = Random.Range(0, 90);
            // Vector3 rot = new Vector3(0, 0, randomZ);
            var butterfly = Instantiate(_zoneDConfig.butterflyPrefab, worldPos, Quaternion.identity);
            butterfly.Initialize(i);
            butterfly.onSelectRightFlower.AddListener(OnRight);
            butterfly.onSelectWrongFlower.AddListener(OnWrong);
            _butterflyList.Add(butterfly);
        }
        for (var j = 0; j < level.flowers.Count; j++)
        {
            if (_zoneDConfig.bottomFlowers[j])
            {
                level.flowers[j].sprite = _zoneDConfig.bottomFlowers[j];
            }
            else
            {
                level.flowers[j].sprite = _zoneDConfig.bottomFlowers[0];
            }
        }
    }



    private void OnDisable()
    {
        foreach (var butterfly in _butterflyList)
        {
            butterfly.onSelectRightFlower.RemoveAllListeners();
            butterfly.onSelectWrongFlower.RemoveAllListeners();
        }
    }


    private void OnRight()
    {
        rightScore++;
        if (HandleAnswer()) return;
        UIManager.Instance.inGameViewInstance.AddToRights(rightScore);
    }


    private void OnWrong()
    {
        wrongScore++;
        if (HandleAnswer()) return;
        UIManager.Instance.inGameViewInstance.AddToWrongs(wrongScore);
    }

    private bool HandleAnswer()
    {
        overallCounter++;

        // if (overallCounter >= _zoneDConfig.FindPathLevel.splines.Count)
        // {
        //     DelayFinishGameBehaviour();
        //     return true;
        // }

        return false;
    }


    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var gameState = Common.GameWinState.Neutral;
        gameState = rightScore >= wrongScore ? Common.GameWinState.Win : Common.GameWinState.Loose;
        var finishData = new Common.LevelFinishData(rightScore, wrongScore,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount, currentConfig.gameName);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }

    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;

        if (rightScore > wrongScore && overallCounter >= flowerCount)
        {
            gameState = Common.GameWinState.Win;
        }
        else if(wrongScore >= rightScore && overallCounter >= flowerCount)
        {
            gameState = Common.GameWinState.Loose;
        }


        var finishData = new Common.LevelFinishData(rightScore, wrongScore,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount, currentConfig.gameName);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
        {
            GameManager.Instance.OnWinGame(finishData);
        }
    }

    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static FindPathGameHandler _instance;

    public static FindPathGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<FindPathGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(FindPathGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as FindPathGameHandler;
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