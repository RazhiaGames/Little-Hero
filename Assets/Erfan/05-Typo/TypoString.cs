using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TypoString : MonoBehaviour
{
    public List<TypoItem> typoItems = new List<TypoItem>();


        [Button]
        public void GetTypoItemsInchildren()
        {
            typoItems.Clear(); // Optional: clear existing list first
            typoItems.AddRange(GetComponentsInChildren<TypoItem>(true));
        }
}
