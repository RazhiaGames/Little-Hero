using System;
using Joyixir.GameManager.Utils;
using UnityEngine;


public class MapQuestionAsker : MonoBehaviour
{
    [Serializable]
    public struct QuestionData
    {
        public string question;
        public string answerOne;
        public string answerTwo;
        public string wrongAnswerPostMortem;
        public string correctAnswerPostMortem;
        public int correctAnswerIndex;
        public string questionID;
    }
    public QuestionData questionData;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerPrefs.HasKey($"{questionData.questionID}_{GMPrefs.ProfileName}")) return;
            PlayerPrefs.SetInt($"{questionData.questionID}_{GMPrefs.ProfileName}", 1);
            UIManager.Instance.ShowQuestion(questionData);
            
        }
    }
}