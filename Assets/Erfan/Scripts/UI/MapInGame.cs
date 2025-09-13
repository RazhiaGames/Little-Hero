using System;
using Joyixir.GameManager.UI;
using Joyixir.GameManager.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapInGame : View
{
    public Button goToMenuButton;
    public Button exitGameButton;
    public Button closeButton;


    private void OnEnable()
    {
        goToMenuButton.onClick.AddListener(goToMenu);
        exitGameButton.onClick.AddListener(exitGame);
        closeButton.onClick.AddListener(closeButtonClicked);
    }


    private void OnDisable()
    {
        goToMenuButton.onClick.RemoveListener(goToMenu);
        exitGameButton.onClick.RemoveListener(exitGame);
        closeButton.onClick.RemoveListener(closeButtonClicked);
    }




    public void goToMenu()
    {
        GMPrefs.SetPlayerPositionAndRotation(MaleCharacter.Instance.transform.position,
            MaleCharacter.Instance.transform.rotation.y);
        SceneManager.LoadScene(0);
    }

    public void exitGame()
    {
        GMPrefs.SetPlayerPositionAndRotation(MaleCharacter.Instance.transform.position,
            MaleCharacter.Instance.transform.rotation.y);
        Application.Quit();
    }

    public void closeButtonClicked()
    {
        GameManager.Instance.EnableController();

        Close();
    }


    protected override void OnBackBtn()
    {
    }
}