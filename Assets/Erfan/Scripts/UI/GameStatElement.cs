using RTLTMPro;
using UnityEngine;

public class GameStatElement : MonoBehaviour
{
    public RTLTextMeshPro gameName;
    public RTLTextMeshPro rightCount;
    public RTLTextMeshPro wrongCount;
    public RTLTextMeshPro time;
    public RTLTextMeshPro checkBtnCount;
    
    public void Initialize(Common.LevelFinishData finishData)
    {
        gameName.text = finishData.gameName;
        rightCount.text = finishData.RightCount.ToString();
        wrongCount.text = finishData.WrongCount.ToString();
        time.text = finishData.TimeCount.ToString();
        checkBtnCount.text = (finishData.checkButtonCount + 1).ToString();
    }
}
