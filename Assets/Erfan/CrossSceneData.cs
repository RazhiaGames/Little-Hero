using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrossSceneData", menuName = "Razhia/CrossSceneData", order = 0)]
public class CrossSceneData : SingletonScriptableObject<CrossSceneData>
{
    public List<Sprite> userImagesList = new List<Sprite>();
}