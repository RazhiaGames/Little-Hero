using System;
using System.Collections.Generic;
using Joyixir.GameManager.UI;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;


public class WonView : View
{
    public Button homeButton;
    public Button restartButton;
    public Button closeButton;
    public Button nextButton;
    public RTLTextMeshPro rightCount;
    public RTLTextMeshPro wrongCount;
    public RTLTextMeshPro time;
    public RTLTextMeshPro checkBtnCounter;
    public GameObject checkBtnParent;
    public GameObject youWinText;
    public GameObject youLooseText;
    public GameObject stillNotWonText;
    

    private void OnEnable()
    {
        homeButton.onClick.AddListener(() => GameManager.Instance.LoadScene(1));
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartCurrentLevel());
        closeButton.onClick.AddListener(() => AnimateDown());
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }



    private void OnDisable()
    {
        homeButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        nextButton.onClick.RemoveAllListeners();

    }

    public void ShowLevelFinishData(Common.LevelFinishData finishData)
    {
        rightCount.text = finishData.RightCount.ToString();
        wrongCount.text = finishData.WrongCount.ToString();

        time.text = StaticUtils.GetRawMinAndSeconds(finishData.TimeCount);
        checkBtnCounter.text = (finishData.checkButtonCount+1).ToString();
        HandleWinState(finishData);
    }

    private void HandleWinState(Common.LevelFinishData finishData)
    {
        youWinText.SetActive(false);
        youLooseText.SetActive(false);
        stillNotWonText.SetActive(false);
        closeButton.gameObject.SetActive(false);
        switch (finishData.gameWinState)
        {
            case Common.GameWinState.Neutral:
                checkBtnParent.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(false);
                stillNotWonText.SetActive(true);
                homeButton.gameObject.SetActive(false);
                restartButton.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(true);
                break;

            case Common.GameWinState.Win:
                checkBtnParent.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(true);
                youWinText.SetActive(true);
                homeButton.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);
                break;

            case Common.GameWinState.Loose:
                checkBtnParent.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(false);
                youLooseText.SetActive(true);
                homeButton.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);


                break;

            default:
                Debug.LogWarning("Unknown game state.");
                break;
        }
    }
    
    private void OnNextButtonClicked()
    {
        var currentConfig = GameManager.Instance.currentLevelConfig;
        var nextConfig = LevelConfigDatabase.Instance.GetNextLevelConfig(currentConfig);
        if (nextConfig != null)
        {
            GameManager.Instance.currentLevelConfig = nextConfig;
            GameManager.Instance.PlayNextLevel();
        }
        else
        {
            UIManager.Instance.ShowText("همه ی بازی ها رو بازی کردی!");
        }
    }

    protected override void OnBackBtn()
    {
    }

}
