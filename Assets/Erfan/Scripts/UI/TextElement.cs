using RTLTMPro;
using UnityEngine;

public class TextElement : MonoBehaviour
{
    public RTLTextMeshPro text;


    public void SetText(string mTex)
    {
        text.text = mTex;
    }
}
