using System;
using Cysharp.Threading.Tasks;
using Joyixir.GameManager.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public LevelConfig currentLevelConfig;
    public Common.Location currentLocation;
    public string currentLocationName;
    public Common.Difficulty currentDifficulty;
    public int gameIndex;
    public GameHandler currentGameHandler;

    protected override void Awake()
    {
        base.Awake();
        TryGetGameHandler();
    }

    private void Start()
    {
        text = $"{currentLocation} - {currentDifficulty}";

    }

    public void OnGameCardClicked(int cardIndex, LevelConfig levelConfig)
    {
        GMPrefs.SetPlayerPositionAndRotation(MaleCharacter.Instance.currentZone.zoneLocation.position, MaleCharacter.Instance.transform.rotation.y);
        GameProgressManager.SetZoneStarted(currentDifficulty, currentLocation);
        currentLevelConfig = levelConfig;
        gameIndex = cardIndex;
        LoadScene(cardIndex + 1);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryGetGameHandler();
    }



    public async UniTaskVoid OnFinishGameAsync(Common.LevelFinishData finishData = null)
    {
        GameProgressManager.MarkGamePlayed(gameIndex-1, currentLocation, currentDifficulty, finishData);
        await UniTask.Delay(1000);
        UIManager.Instance.ShowYouWon(finishData);
    }

    public void OnWinGame(Common.LevelFinishData finishData)
    {
        Timer.Instance.enabled = false;
        GameProgressManager.MarkGamePlayed(gameIndex-1, currentLocation, currentDifficulty, finishData);
        if (GameProgressManager.AreAllGamesCompleted(currentLocation, currentDifficulty))
        {
            GameProgressManager.DeleteZoneStarted(currentDifficulty);
            UIManager.Instance.ShowStatisticsView(currentDifficulty, currentLocation);
        }
    }

    public string text;

    public void LoadScene(int sceneIndex)
    {
        UIManager.Instance.CloseAllWindows();
        SceneManager.LoadScene(sceneIndex);
        text = $"{currentLocation} - {currentDifficulty}";

    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        text = $"{currentLocation} - {currentDifficulty}";

    }

    public void PlayNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var sceneCount = SceneManager.sceneCountInBuildSettings;
        gameIndex++;
        if (currentSceneIndex + 1 < sceneCount)
        {
            LoadScene(currentSceneIndex + 1);
        }

    }
    
    public void DisableController()
    {
        if (MaleCharacter.Instance != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            MaleCharacter.Instance.GetComponent<movement>().enabled = false;
            MaleCharacter.Instance.GetComponent<Animator>().SetBool("MoveFWD", false);
        }
    }
    
    public void EnableController()
    {
        if (MaleCharacter.Instance != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            MaleCharacter.Instance.GetComponent<movement>().enabled = true;
            MaleCharacter.Instance.GetComponent<Animator>().SetBool("MoveFWD", true);
        }

    }
    
    private void TryGetGameHandler()
    {
        try
        {
            currentGameHandler = FindObjectsByType<GameHandler>(FindObjectsSortMode.None)[0];
        }
        catch (IndexOutOfRangeException)
        {
            // Handle the case where no GameHandler is found
            Debug.LogWarning("No GameHandler found in the scene.");
            currentGameHandler = null; // Or handle it differently based on your needs
        }
    }
    
    
#if UNITY_EDITOR
    public float x=10, y=10, width=30, height=50;
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 30; // Set the font size
        style.normal.textColor = Color.white; // Optional: set text color
        style.alignment = TextAnchor.MiddleCenter; // Optional: set alignment
        GUI.color = Color.black;
        GUI.Label(new Rect(x, y, width, height), text, style);
    }
#endif
}