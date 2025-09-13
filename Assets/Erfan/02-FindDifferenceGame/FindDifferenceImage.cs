using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class FindDifferenceImage : MonoBehaviour
{
    public List<DifferenceItem> diffItems = new List<DifferenceItem>();
    public int countToGuessToWin;

    [Button]
    public void GetItems()
    {
        // Get all DifferenceItem components in children (including inactive ones if needed)
        diffItems = GetComponentsInChildren<DifferenceItem>(true).ToList();
    }

    [Button]
    public void UpdateCount()
    {
        countToGuessToWin = diffItems.Count;
    }
}
