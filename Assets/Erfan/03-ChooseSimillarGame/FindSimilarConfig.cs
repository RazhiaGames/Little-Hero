using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "FindSimilarConfig", menuName = "Erfan/Scriptable Objects/FindSimilarConfig")]
public class FindSimilarConfig : LevelConfig
{
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        [FoldoutGroup("$GroupName")]public Common.Location location;
        [FoldoutGroup("$GroupName")]public Common.Difficulty difficulty;
        [FoldoutGroup("$GroupName")]public string howToPlayText;
        [FoldoutGroup("$GroupName")]public AudioClip howToPlayAudio;


        [FoldoutGroup("$GroupName")]public FindSimilarPrefab findSimilarPrefab;


        private string GroupName => $"{location} - {difficulty}";
        // public List<ChooseSimilarItem> GetShuffledCombinedList()
        // {
        //     var combined = bottomWrongItems.Concat(bottomCorrectItems).ToList();
        //     return combined.OrderBy(_ => Random.value).ToList();
        // }
    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
}