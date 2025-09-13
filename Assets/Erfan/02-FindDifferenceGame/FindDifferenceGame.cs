using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "FindDifferenceGame", menuName = "Erfan/Scriptable Objects/FindDifferenceGame")]
public class FindDifferenceGame : LevelConfig
{
    [ListDrawerSettings()]
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        [FoldoutGroup("$GroupName", false)] public Common.Location location;
        [FoldoutGroup("$GroupName")] public Common.Difficulty difficulty;
        [FoldoutGroup("$GroupName")] public FindDifferenceImage prefab;
        [FoldoutGroup("$GroupName")] public Color levelRightSpriteColor = Color.yellow;
        [GUIColor("cyan"), FoldoutGroup("$GroupName")] public string howToPlayText;
        [FoldoutGroup("$GroupName")]public AudioClip howToPlayAudio;

        private string GroupName => $"{location} - {difficulty}";

    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
}