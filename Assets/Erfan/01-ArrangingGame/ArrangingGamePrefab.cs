using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ArrangingGamePrefab : MonoBehaviour
{
    public List<DragObject> items = new List<DragObject>();
    
    [Button]
    public void GetTypoItemsInchildren()
    {
        items.Clear(); // Optional: clear existing list first
        items.AddRange(GetComponentsInChildren<DragObject>(true));
    }
}
