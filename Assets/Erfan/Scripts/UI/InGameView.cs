using Joyixir.GameManager.UI;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;


public class InGameView : View
{
    public RTLTextMeshPro userRights;
    public RTLTextMeshPro userWrongs;
    public GameObject wrongsGo;
    public Timer timer;
    public Button restartButton;
    public Button homeButton;
    public Button howToPlayButton;
    public Button checkFinishButton;

    public int totalRights;
    public int checkButtonCount;
    public Transform rightAndWrongContainer;
    private void OnEnable()
    {
        homeButton.onClick.AddListener(() => GameManager.Instance.LoadScene(1));
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartCurrentLevel());
        howToPlayButton.onClick.AddListener(() =>
            UIManager.Instance.ShowHowToPlay());

        checkFinishButton.onClick.AddListener(() =>
        {
            GameManager.Instance.currentGameHandler.CheckForFinish();
            checkButtonCount++;
        });
    }

    private void OnDisable()
    {
        homeButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        howToPlayButton.onClick.RemoveAllListeners();
        checkFinishButton.onClick.RemoveAllListeners();
    }


    public void Initialize(int mTotalRights = 0)
    {
        userRights.text = "0";
        userWrongs.text = "0";
        totalRights = mTotalRights;
        if (totalRights > 0)
        {
            userRights.text = $"{0} / {totalRights}";
        }
    }

    protected override void OnBackBtn()
    {
    }

    public void AddToRights(int score)
    {
        userRights.text = score.ToString();
        if (totalRights > 0)
        {
            userRights.text = $"{score} / {totalRights}";
        }
    }

    public void AddToWrongs(int wrongs)
    {
        userWrongs.text = wrongs.ToString();
    }

    public void StartTimer()
    {
        timer.StartTimer();
    }

    public void HideWrongs()
    {
        wrongsGo.SetActive(false);
    }

    public void HideRightAndWrongContainer()
    {
        rightAndWrongContainer.gameObject.SetActive(false);
    }
}