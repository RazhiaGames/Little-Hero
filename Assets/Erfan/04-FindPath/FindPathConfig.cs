using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "FindPathConfig", menuName = "Erfan/Scriptable Objects/FindPathConfig")]
public class FindPathConfig : LevelConfig
{
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        [FoldoutGroup("$GroupName", false)]public Common.Location location;
        [FoldoutGroup("$GroupName")] public Common.Difficulty difficulty;
        [FoldoutGroup("$GroupName")] public Butterfly butterflyPrefab;
        [FoldoutGroup("$GroupName")] public string howToPlayText;
        [FoldoutGroup("$GroupName")]public AudioClip howToPlayAudio;


        [FoldoutGroup("$GroupName")] public FindPathLevel FindPathLevel;
        [FoldoutGroup("$GroupName")] public List<Sprite> bottomFlowers = new List<Sprite>();
        private string GroupName => $"{location} - {difficulty}";

    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
}