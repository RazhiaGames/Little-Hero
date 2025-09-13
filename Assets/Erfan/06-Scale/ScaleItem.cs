using EPOOutline;
using Sirenix.OdinInspector;
using UnityEngine;

[SelectionBase]
public class ScaleItem : MonoBehaviour
{
    public bool isWrongScale;
    public Outlinable rightOutlinable;
    public Outlinable wrongOutlinable;

    [Button]
    public void EnableRightOutlinable()
    {
        rightOutlinable.enabled = true;
    }

    [Button]
    public void EnableWrongOutlinable()
    {
        wrongOutlinable.enabled = true;
    }


    public void GetOutlinables()
    {
        var d = gameObject.GetComponents<Outlinable>();
        rightOutlinable = d[0];
        wrongOutlinable = d[1];
    }
}
