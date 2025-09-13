using System;
using Joyixir.GameManager.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectNumberView : View
{
    public Button firstButton;
    public Button secondButton;
    public Button thirdButton;
    public Button fourthButton;
    public Button fifthButton;

    public UnityEvent<int> onClick;
    private void OnEnable()
    {
        firstButton.onClick.AddListener(() => OnNumberButtonClicked(0));
        secondButton.onClick.AddListener(() => OnNumberButtonClicked(1));
        thirdButton.onClick.AddListener(() => OnNumberButtonClicked(2));
        fourthButton.onClick.AddListener(() => OnNumberButtonClicked(3));
        fifthButton.onClick.AddListener(() => OnNumberButtonClicked(4));
    }

    private void OnDisable()
    {
        firstButton.onClick.RemoveAllListeners();
        secondButton.onClick.RemoveAllListeners();
        thirdButton.onClick.RemoveAllListeners();
        fourthButton.onClick.RemoveAllListeners();
        fifthButton.onClick.RemoveAllListeners();
    }

    private void OnNumberButtonClicked(int number)
    {
        Debug.Log($"Button {number + 1} clicked!");
        onClick?.Invoke(number);
        gameObject.SetActive(false);
    }

    protected override void OnBackBtn()
    {
    }
}