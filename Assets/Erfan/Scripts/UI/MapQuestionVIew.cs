using System;
using Cysharp.Threading.Tasks;
using Joyixir.GameManager.UI;
using Joyixir.GameManager.Utils;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;


public class MapQuestionVIew : View
{
    public RTLTextMeshPro question;
    public RTLTextMeshPro answerOne;
    public RTLTextMeshPro answerTwo;
    public RTLTextMeshPro wrongAnswerPostMortem;
    public RTLTextMeshPro rightAnswerCompliment;

    public Button answerOneBtn;
    public Button answerTwoBtn;
    public Button closeBtn;
    public VoidChannelEventSO onGetStar;

    private int _correctAnswerIndex;


    public void Initialize(MapQuestionAsker.QuestionData data)
    {
        GameManager.Instance.DisableController();
        question.text = data.question;
        answerOne.text = data.answerOne;
        answerTwo.text = data.answerTwo;
        _correctAnswerIndex = data.correctAnswerIndex;
        wrongAnswerPostMortem.text = data.wrongAnswerPostMortem;
        rightAnswerCompliment.text = data.correctAnswerPostMortem;
        wrongAnswerPostMortem.gameObject.SetActive(false);
        rightAnswerCompliment.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        answerOneBtn.onClick.AddListener(OnClickAnswerOne);
        answerTwoBtn.onClick.AddListener(OnClickAnswerTwo);
        closeBtn.onClick.AddListener(OnCLoseBtn);
    }


    private void OnDisable()
    {
        answerOneBtn.onClick.RemoveListener(OnClickAnswerOne);
        answerTwoBtn.onClick.RemoveListener(OnClickAnswerTwo);
        closeBtn.onClick.RemoveListener(OnCLoseBtn);
    }


    private void OnClickAnswerOne()
    {
        if (_correctAnswerIndex == 0)
        {
            Debug.Log("WON");
            Win();
        }
        else
        {
            Loose();
            Debug.Log("Loose");
        }
    }

    private void OnClickAnswerTwo()
    {
        if (_correctAnswerIndex == 1)
        {
            Win();
        }
        else
        {
            Loose();
        }
    }


    private async void Win()
    {
        Debug.Log("WON");
        closeBtn.gameObject.SetActive(true);
        question.gameObject.SetActive(false);
        answerOne.gameObject.SetActive(false);
        answerTwo.gameObject.SetActive(false);
        wrongAnswerPostMortem.gameObject.SetActive(false);
        answerOneBtn.gameObject.SetActive(false);
        answerTwoBtn.gameObject.SetActive(false);
        wrongAnswerPostMortem.gameObject.SetActive(false);
        rightAnswerCompliment.gameObject.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        await AnimateDown();
        onGetStar.RaiseEvent();
        GMPrefs.StarCount++;
    }

    private void Loose()
    {
        Debug.Log("Loose");
        closeBtn.gameObject.SetActive(true);
        question.gameObject.SetActive(false);
        answerOne.gameObject.SetActive(false);
        answerTwo.gameObject.SetActive(false);
        wrongAnswerPostMortem.gameObject.SetActive(false);
        answerOneBtn.gameObject.SetActive(false);
        answerTwoBtn.gameObject.SetActive(false);
        if (wrongAnswerPostMortem.text != "")
            wrongAnswerPostMortem.gameObject.SetActive(true);
    }


    public override void Hide()
    {
        GameManager.Instance.EnableController();
        base.Hide();
    }

    private void OnCLoseBtn()
    {
        AnimateDown();
        GameManager.Instance.EnableController();
    }


    protected override void OnBackBtn()
    {
    }
}