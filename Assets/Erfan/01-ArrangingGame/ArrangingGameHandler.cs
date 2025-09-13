using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RTLTMPro;
using UnityEngine;

public class ArrangingGameHandler : GameHandler
{
    public Transform spawnPoint;
    public int inBoxCount = 0;
    public int wrongInBoxCount = 0;
    private List<DragObject> items = new List<DragObject>();
    private ArrangingGameConfig.ZoneDifficultyConfig _zoneDConfig;
    private ArrangingGameConfig currentConfig;
    
    public Container firstContainer;
    public Container secondContainer;
    public Container thirdContainer;
    public Container fourthContainer;
    public Container fifthContainer;
    

    public Transform containersParent;
    private void Start()
    {
        currentConfig = GameManager.Instance.currentLevelConfig as ArrangingGameConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        
        zoneHowToPlayText = _zoneDConfig.howToPlayText;
        zoneHowToPlayAudio = _zoneDConfig.howToPlayAudio;

        firstContainer.Initialize(_zoneDConfig.firstContainerText, _zoneDConfig.containerTypeFirst);
        secondContainer.Initialize(_zoneDConfig.secondContainerText, _zoneDConfig.containerTypeSecond);
        thirdContainer.Initialize(_zoneDConfig.thirdContainerText, _zoneDConfig.containerTypeThird);

        if (GameManager.Instance.currentDifficulty == Common.Difficulty.Hard)
        {
            containersParent.position = new Vector3(
                5.63f, containersParent.position.y, containersParent.position.z);
            fourthContainer.gameObject.SetActive(true);
            fifthContainer.gameObject.SetActive(true);
            fourthContainer.Initialize(_zoneDConfig.fourthContainerText, _zoneDConfig.containerTypeFourth);
            fifthContainer.Initialize(_zoneDConfig.fifthContainerText, _zoneDConfig.containerTypeFifth);
        }
        
        var prefabIns = Instantiate(_zoneDConfig.prefab, spawnPoint);
        items = prefabIns.items;
        

        UIManager.Instance.HowToPlayAndInGameProcedure(_zoneDConfig.howToPlayText, _zoneDConfig.howToPlayAudio,
            () => { UpdateScore(); });
    }


    public void UpdateScore()
    {
        UIManager.Instance.inGameViewInstance.HideRightAndWrongContainer();
        UIManager.Instance.inGameViewInstance.AddToRights(inBoxCount);
        UIManager.Instance.inGameViewInstance.AddToWrongs(wrongInBoxCount);
        
        // inBoxText.text = "تعداد صحیح: " + inBoxCount;
        // if (inBoxCount >= items.Count)
        // {
        //     DelayFinishGameBehaviour();
        // }
    }

    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var finishData = new Common.LevelFinishData(inBoxCount, 0,
            (int)Timer.Instance.timeRemaining, Common.GameWinState.Win, checkBtnCount, currentConfig.gameName);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }

    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;
        if (inBoxCount >= items.Count  && wrongInBoxCount <= 2)
        {
            gameState = Common.GameWinState.Win;    
        }

        if (GameManager.Instance.currentDifficulty == Common.Difficulty.Hard)
        {
            if (inBoxCount >= items.Count - 2  && wrongInBoxCount <= 2)
            {
                gameState = Common.GameWinState.Win;    
            }
        }

        var finishData = new Common.LevelFinishData(inBoxCount, wrongInBoxCount,
            (int)Timer.Instance.timeRemaining, gameState,checkBtnCount, currentConfig.gameName);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
        {
            GameManager.Instance.OnWinGame(finishData);
        }
    }

    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static ArrangingGameHandler _instance;

    public static ArrangingGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ArrangingGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(ArrangingGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as ArrangingGameHandler;
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