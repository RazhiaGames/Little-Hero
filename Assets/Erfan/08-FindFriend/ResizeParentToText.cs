using UnityEngine;
using TMPro;

[ExecuteAlways]
public class ResizeParentToText : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public RectTransform parent;
    public float xAdder;
    void Update()
    {
        if (tmp == null || parent == null) return;

        tmp.ForceMeshUpdate();
        var textSize = tmp.GetRenderedValues(false);
        parent.sizeDelta = new Vector2(textSize.x+xAdder, textSize.y+xAdder);
    }
}