using Cysharp.Threading.Tasks;
using UnityEngine;

public class FindDifferenceGameHandler : GameHandler
{
    public Transform prefabParent;
    public Camera mainCamera;

    private int _diffCount;
    private int _foundCount = 0;
    private FindDifferenceGame.ZoneDifficultyConfig _zoneDConfig;
    public Color levelRightSpriteColor;
    private FindDifferenceGame currentConfig;
    private bool _gameInitiated = false;
    private FindDifferenceImage findDifferenceImage;
    private void Start()
    {
        currentConfig = GameManager.Instance.currentLevelConfig as FindDifferenceGame;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        zoneHowToPlayText = _zoneDConfig.howToPlayText;
        zoneHowToPlayAudio = _zoneDConfig.howToPlayAudio;

        findDifferenceImage = _zoneDConfig.prefab;
        levelRightSpriteColor = _zoneDConfig.levelRightSpriteColor;
        var instance = Instantiate(findDifferenceImage, prefabParent);
        instance.transform.localPosition = Vector3.zero;
        _diffCount = instance.diffItems.Count;
        UIManager.Instance.HowToPlayAndInGameProcedure(_zoneDConfig.howToPlayText,_zoneDConfig.howToPlayAudio,
            () =>
            {
                UIManager.Instance.inGameViewInstance.HideWrongs();
                _gameInitiated = true;
                UpdateText();
            });
    }


    void Update()
    {
        if (!_gameInitiated) return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Clicked on: " + hit.collider.name);

                // Optional: check for a specific component
                DifferenceItem item = hit.collider.GetComponentInParent<DifferenceItem>();
                if (item != null)
                {
                    item.OnFound();
                    _foundCount++;
                    UpdateText();
                    // if (_foundCount >= _diffCount)
                    // {
                    //     Debug.Log("Level Won");
                    //     DelayFinishGameBehaviour();
                    // }
                }
            }
        }
    }

    private void UpdateText()
    {
        UIManager.Instance.inGameViewInstance.userRights.text = $"{_foundCount}/{findDifferenceImage.countToGuessToWin}";
    }

    private async UniTaskVoid DelayFinishGameBehaviour()
    {
        await UniTask.DelayFrame(30);
        var finishData = new Common.LevelFinishData(_foundCount, 0,
            (int)Timer.Instance.timeRemaining, Common.GameWinState.Win, checkBtnCount, currentConfig.gameName);
        GameManager.Instance.OnFinishGameAsync(finishData);
    }

    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;

        if (_foundCount >= findDifferenceImage.countToGuessToWin)
        {
            gameState = Common.GameWinState.Win;
        }

        var finishData = new Common.LevelFinishData(_foundCount, 0,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount, currentConfig.gameName);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
        {
            GameManager.Instance.OnWinGame(finishData);
        }
    }

    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static FindDifferenceGameHandler _instance;

    public static FindDifferenceGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<FindDifferenceGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(FindDifferenceGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as FindDifferenceGameHandler;
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