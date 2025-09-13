using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ChooseSimilarGameHandler : GameHandler
{
    public Transform spawnPoint;
    public Camera mainCamera;
    private ChooseSimilarItem sampleItem;

    private int rights;
    private int wrongs;

    private FindSimilarConfig.ZoneDifficultyConfig _zoneDConfig;
    private FindSimilarConfig currentConfig;
    private void Start()
    {
        currentConfig = GameManager.Instance.currentLevelConfig as FindSimilarConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        zoneHowToPlayText = _zoneDConfig.howToPlayText;
        zoneHowToPlayAudio = _zoneDConfig.howToPlayAudio;



        var prefabIns = Instantiate(_zoneDConfig.findSimilarPrefab, spawnPoint);

        sampleItem = prefabIns.sampleItem;

        UIManager.Instance.HowToPlayAndInGameProcedure(_zoneDConfig.howToPlayText,_zoneDConfig.howToPlayAudio);
    }


    private async void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var isPor = hit.collider.TryGetComponent<ChooseSimilarItem>(out var rayCastItem);
                if (!isPor)
                {
                    hit.collider.transform.parent.TryGetComponent<ChooseSimilarItem>(out rayCastItem);
                }
                if (rayCastItem == null) return;
                if (rayCastItem.itemType == sampleItem.itemType && !rayCastItem.isWrongScale)
                {
                    rights++;
                    StaticTweeners.AnimateDown(rayCastItem.transform);
                    UIManager.Instance.inGameViewInstance.AddToRights(rights);
                    rayCastItem.EnableRightOutlinable();

                    rayCastItem.enabled = false;
                    // if (rights >= _zoneDConfig.bottomCorrectItems.Count)
                    // {
                    //     DelayFinishGameBehaviour();
                    // }
                }
                else
                {
                    wrongs++;
                    var isColPor = rayCastItem.gameObject.TryGetComponent<Collider>(out var col);
                    if (isColPor)
                        col.enabled = false;
                    else
                    {
                        rayCastItem.transform.GetChild(0).gameObject.TryGetComponent<Collider>(out col);
                        if (col != null)
                            col.enabled = false;
                    }
                    StaticTweeners.DoYoyoScale(rayCastItem.transform);
                    UIManager.Instance.inGameViewInstance.AddToWrongs(wrongs);
                    rayCastItem.EnableWrongOutlinable();
                }
            }
        }
    }


    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var gameState = Common.GameWinState.Neutral;
        gameState = rights >= wrongs ? Common.GameWinState.Win : Common.GameWinState.Loose;
        
        var finishData = new Common.LevelFinishData(rights, wrongs,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount, currentConfig.gameName);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }
    
    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;
        
        if (rights >= _zoneDConfig.findSimilarPrefab.bottomCorrectItems.Count)
        {
            gameState = Common.GameWinState.Win;

        }

        var finishData = new Common.LevelFinishData(rights, wrongs,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount, currentConfig.gameName);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
        {
            GameManager.Instance.OnWinGame(finishData);
        }
    }
    
    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static ChooseSimilarGameHandler _instance;

    public static ChooseSimilarGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ChooseSimilarGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(ChooseSimilarGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as ChooseSimilarGameHandler;
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