using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ScaleConfig", menuName = "Erfan/Scriptable Objects/ScaleConfig")]
public class ScaleConfig : LevelConfig      
{
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        [FoldoutGroup("$GroupName")]public Common.Location location;
        [FoldoutGroup("$GroupName")]public Common.Difficulty difficulty;
        [FoldoutGroup("$GroupName")]public ScalePrefab prefab;
        [FoldoutGroup("$GroupName")]public string howToPlayText;
        [FoldoutGroup("$GroupName")]public AudioClip howToPlayAudio;

        private string GroupName => $"{location} - {difficulty}";

    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
}
