using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[SelectionBase]
public class NumbersGameDragObjectSchool : NumbersGameDragObject
{
    public int number;
    public float offsetAmount = -1.3f;
    public List<GameObject> numbersGameItem = new List<GameObject>();
    public Shader refShader;
    public bool isValidate = false;


    private void OnValidate()
    {
        if (isValidate)
            MakeNumber();
    }

    [Button]
    public void MakeNumber()
    {
        transform.Clear(); // Assumes an extension method that clears all children
        numbersGameItem.Clear();

        string numberStr = number.ToString();
        float offsetRef;

        for (int i = 0; i < numberStr.Length; i++)
        {
            offsetRef = offsetAmount * i;
            int digit = numberStr[i] - '0'; // Convert char to int
            var digitGo = GS.INS.GetDigit(digit);

#if UNITY_EDITOR
            var ins = (GameObject)PrefabUtility.InstantiatePrefab(digitGo, transform);
#else
            var ins = Instantiate(digitGo, transform);
#endif

            ins.transform.localPosition = new Vector3(offsetRef, 0, 0); // LocalPosition for prefab children
            numbersGameItem.Add(ins);
        }

        numbersGameItemType = (number % 2 == 0)
            ? Common.NumbersGameItemType.even
            : Common.NumbersGameItemType.odd;

        gameObject.name = number.ToString();

        Material refMat = new Material(refShader);
        for (var i = 0; i < numbersGameItem.Count; i++)
        {
            if (i == 0)
            {
                refMat = numbersGameItem[i].GetComponentInChildren<MeshRenderer>().sharedMaterial;
                continue;
            }

            numbersGameItem[i].GetComponentInChildren<MeshRenderer>().sharedMaterial = refMat;
        }
    }
}