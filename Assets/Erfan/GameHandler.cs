using UnityEngine;

public class GameHandler : MonoBehaviour
{
    protected int checkBtnCount = 0;
    public string zoneHowToPlayText;
    public AudioClip zoneHowToPlayAudio;
    public virtual void CheckForFinish()
    {
        checkBtnCount = UIManager.Instance.inGameViewInstance.checkButtonCount;
    }
}
