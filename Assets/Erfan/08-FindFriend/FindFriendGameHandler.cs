using UnityEngine;
using Image = UnityEngine.UI.Image;

public class FindFriendGameHandler : GameHandler
{
    private FindFriendConfig.ZoneDifficultyConfig _zoneDConfig;
    public int _wrongCounter;
    public int _rightCounter;
    private FindFriendConfig.Friend sampleFriend;
    FindFriendConfig currentConfig;
    // public Image friendImage;
    private void Start()
    {
        
        currentConfig = GameManager.Instance.currentLevelConfig as FindFriendConfig;
        _zoneDConfig = currentConfig.GetConfig(GameManager.Instance.currentLocation,
            GameManager.Instance.currentDifficulty);
        zoneHowToPlayText = _zoneDConfig.howToPlayText;
        zoneHowToPlayAudio = _zoneDConfig.howToPlayAudio;

        var findFriendView = UIManager.Instance.ShowFindFriendView();
        findFriendView.Initialize(_zoneDConfig);
        sampleFriend = _zoneDConfig.sampleFriend;
        UIManager.Instance.HowToPlayAndInGameProcedure(_zoneDConfig.howToPlayText,_zoneDConfig.howToPlayAudio,
            () => {  });
    }
    
    
    
    public void OnClickFriend(FindFriendConfig.Friend friend)
    {
        if (friend.FriendType == sampleFriend.FriendType && friend.FriendEmotion == sampleFriend.FriendEmotion)
        {
            UIManager.Instance.inGameViewInstance.AddToRights(1);
            _rightCounter++;
        }
        else
        {
            _wrongCounter++;
            UIManager.Instance.inGameViewInstance.AddToWrongs(_wrongCounter);
        }
    }
    
    
    public override void CheckForFinish()
    {
        base.CheckForFinish();
        var gameState = Common.GameWinState.Neutral;
        if (_rightCounter>0 && _wrongCounter<=0)
        {
            gameState = Common.GameWinState.Win;
        }
        else
        {
            gameState = Common.GameWinState.Loose;
        }

        var finishData = new Common.LevelFinishData(_rightCounter, _wrongCounter,
            (int)Timer.Instance.timeRemaining, gameState, checkBtnCount, currentConfig.gameName);
        UIManager.Instance.ShowYouWon(finishData);
        if (gameState == Common.GameWinState.Win)
        {
            GameManager.Instance.OnWinGame(finishData);
        }
    }

    #region Singleton

    public bool isDontDestroyOnLoad = false;
    private static FindFriendGameHandler _instance;

    public static FindFriendGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<FindFriendGameHandler>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(FindFriendGameHandler)} found in the scene.");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as FindFriendGameHandler;
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
