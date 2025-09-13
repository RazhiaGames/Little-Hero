using Joyixir.GameManager.UI;
using Joyixir.GameManager.Utils;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserRegisterView : View
{
    public Button closeButton;
    public TMP_InputField profileNameInput;
    private int _profilePicIndex;
    public Button doneButton;
    private void OnEnable()
    {
        closeButton.onClick.AddListener(() => { AnimateDown(); });
        doneButton.onClick.AddListener(SaveProfileAndCloseWindow);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
        doneButton.onClick.RemoveListener(SaveProfileAndCloseWindow);

    }

    public void SetProfilePictureIndex(int index)
    {
        _profilePicIndex = index;
    }

    public async void SaveProfileAndCloseWindow()
    {
        GMPrefs.ProfileName = profileNameInput.text;
        GMPrefs.ProfilePicIndex = _profilePicIndex;
        await AnimateDown();
		SceneManager.LoadScene("ADHDGAME");

    }


    protected override void OnBackBtn()
    {
    }
}