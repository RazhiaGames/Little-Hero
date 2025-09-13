using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScalePrefab : MonoBehaviour
{
    public List<ScaleItem> rightScaleItems = new List<ScaleItem>();
    public List<ScaleItem> wrongScaleItems = new List<ScaleItem>();
    public ScaleItem sampleScaleItem;
    public Transform itemsTf;
    [Button]
    public void GetItemsInChildren()
    {
        rightScaleItems.Clear();
        wrongScaleItems.Clear();
        var items = itemsTf.GetComponentsInChildren<ScaleItem>();
        foreach (var item in items)
        {
            if (item.isWrongScale)
            {
                wrongScaleItems.Add(item);
            }
            else
            {
                rightScaleItems.Add(item);
            }

            item.GetOutlinables();
        }
    }
}