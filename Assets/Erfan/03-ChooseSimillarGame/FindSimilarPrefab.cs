using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FindSimilarPrefab : MonoBehaviour
{
    public List<ChooseSimilarItem> bottomWrongItems = new List<ChooseSimilarItem>();
    public List<ChooseSimilarItem> bottomCorrectItems = new List<ChooseSimilarItem>();
    public ChooseSimilarItem sampleItem;
    public Transform itemsTf;

    [Button]
    public void GetItems()
    {
        bottomWrongItems.Clear();
        bottomCorrectItems.Clear();
        var allItems = itemsTf.GetComponentsInChildren<ChooseSimilarItem>(true);

        foreach (var item in allItems)
        {
            if (item.itemType == sampleItem.itemType && !item.isWrongScale)
            {
                bottomCorrectItems.Add(item);
            }
            else
            {
                bottomWrongItems.Add(item);
            }
        }
    }
}
