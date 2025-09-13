using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TypoGameHandler : GameHandler
{
    [SerializeField] private RectTransform stringParent;
    private List<TypoItem> _typoItems = new List<TypoItem>();
    public int rightScore;
    public int wrongScore;
    public int delayFrameCount = 30;
    private RectTransform sentenceRect;
    private int _totalWrongCount = 0;
    private TypoConfig.ZoneDifficultyConfig _zoneDConfig;
    TypoConfig currentConfig;
    private void Start()
    {
        currentConfig = GameManager.Instance.currentLevelConfig as TypoConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation, 
            GameManager.Instance.currentDifficulty);
        zoneHowToPlayText = _zoneDConfig.howToPlayText;
        zoneHowToPlayAudio = _zoneDConfig.howToPlayAudio;

        GetTotalWrongs();
        UIManager.Instance.HowToPlayAndInGameProcedure(_zoneDConfig.howToPlayText,_zoneDConfig.howToPlayAudio,
            () =>
            {
                var typoString = Instantiate(_zoneDConfig.typoString, stringParent);
                sentenceRect = typoString.transform as RectTransform;
                var typoItems = typoString.typoItems;
                foreach (var mTypoItem in typoItems)
                {
                    mTypoItem.onClickButton.AddListener(OnTypoClicked);
                    _typoItems.Add(mTypoItem);
                }
            }, _totalWrongCount);
    }

    private void GetTotalWrongs()
    {
        var typoItems = _zoneDConfig.typoString.typoItems;
        foreach (var typoItem in typoItems)
        {
            if (typoItem.isWrong)
            {
                _totalWrongCount++;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var mTypoItem in _typoItems)
        {
            mTypoItem.onClickButton.RemoveAllListeners();
        }
    }

    private void OnTypoClicked(bool isCorrect)
    {
        if (isCorrect)
            OnRight();
        else
            OnWrong();

        DelayRefreshLayout().Forget();
    }

    public async UniTaskVoid DelayRefreshLayout()
    {
        await UniTask.DelayFrame(delayFrameCount);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sentenceRect);
    }


    private void OnRight()
    {
        rightScore++;
        UIManager.Instance.inGameViewInstance.AddToRights(rightScore);
        // if (rightScore >= _totalWrongCount)
        //     DelayFinishGameBehaviour().Forget();

    }


    private void OnWrong()
    {
        wrongScore++;
        UIManager.Instance.inGameViewInstance.AddToWrongs(wrongScore);
    }




    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var gameState = Common.GameWinState.Neutral;
        gameState = rightScore >= wrongScore ? Common.GameWinState.Win : Common.GameWinState.Loose;
        var finishData = new Common.LevelFinishData(rightScore, wrongScore,
            (int)Timer.Instance.timeRemaining,gameState , checkBtnCount, currentConfig.gameName);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }
    
    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;
        if (rightScore >= _totalWrongCount && rightScore > wrongScore)
            gameState = Common.GameWinState.Win;
        else
            gameState = Common.GameWinState.Loose;

        var finishData = new Common.LevelFinishData(rightScore, wrongScore,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount, currentConfig.gameName);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
            GameManager.Instance.OnWinGame(finishData);
    }
    
    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static TypoGameHandler _instance;

    public static TypoGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<TypoGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(TypoGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as TypoGameHandler;
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