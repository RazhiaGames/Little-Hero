using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NumbersGameConfig", menuName = "Erfan/Scriptable Objects/NumbersGameConfig")]
public class NumbersGameConfig : LevelConfig
{
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        [FoldoutGroup("$GroupName")]public Common.Location location;
        [FoldoutGroup("$GroupName")]public Common.Difficulty difficulty;
        [FoldoutGroup("$GroupName")]public NumbersGamePrefab numbersGamePrefab;
        [FoldoutGroup("$GroupName")]public bool isRange = false;
        [FoldoutGroup("$GroupName")][ShowIf("isRange")] public int rangeMin, rangeMax;
        [FoldoutGroup("$GroupName")]public string howToPlayText;
        [FoldoutGroup("$GroupName")]public AudioClip howToPlayAudio;

        [FoldoutGroup("$GroupName")]public int numToWin = 3;

        [FoldoutGroup("$GroupName")]public List<ContainerTypeClass> containerTypes = new List<ContainerTypeClass>();
        private string GroupName => $"{location} - {difficulty}";


    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
    
    [Serializable]
    public class ContainerTypeClass
    {
        public Common.NumbersGameItemType containerType;
        public string containerText;


    }
}