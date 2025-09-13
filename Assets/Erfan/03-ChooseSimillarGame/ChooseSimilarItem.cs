using EPOOutline;
using Sirenix.OdinInspector;
using UnityEngine;

[SelectionBase]
public class ChooseSimilarItem : MonoBehaviour
{
    public Common.ChooseSimilarGameItemType itemType;
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



}
