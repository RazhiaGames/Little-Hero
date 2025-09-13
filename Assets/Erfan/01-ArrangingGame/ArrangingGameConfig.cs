using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;




[CreateAssetMenu(fileName = "ArrangingGameItems", menuName = "Erfan/Scriptable Objects/ArrangingGameItems")]
public class ArrangingGameConfig : LevelConfig
{
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        [FoldoutGroup("$GroupName")] public Common.Location location;
        [FoldoutGroup("$GroupName")] public Common.Difficulty difficulty;
        [FoldoutGroup("$GroupName")] public ArrangingGamePrefab prefab;
        [FoldoutGroup("$GroupName")] public string howToPlayText;
        [FoldoutGroup("$GroupName")] public AudioClip howToPlayAudio;

        [FoldoutGroup("$GroupName")] public Common.ArrangingGameItemType containerTypeFirst;
        [FoldoutGroup("$GroupName")] public Common.ArrangingGameItemType containerTypeSecond;
        [FoldoutGroup("$GroupName")] public Common.ArrangingGameItemType containerTypeThird;

        [FoldoutGroup("$GroupName")] [ShowIf("IsHardDifficulty")] [FoldoutGroup("$GroupName")]
        public Common.ArrangingGameItemType containerTypeFourth;

        [FoldoutGroup("$GroupName")] [ShowIf("IsHardDifficulty")] [FoldoutGroup("$GroupName")]
        public Common.ArrangingGameItemType containerTypeFifth;

        [FoldoutGroup("$GroupName")] public string firstContainerText;
        [FoldoutGroup("$GroupName")] public string secondContainerText;
        [FoldoutGroup("$GroupName")] public string thirdContainerText;

        [FoldoutGroup("$GroupName")] [ShowIf("IsHardDifficulty")]
        public string fourthContainerText;

        [FoldoutGroup("$GroupName")] [ShowIf("IsHardDifficulty")]
        public string fifthContainerText;

        private string GroupName => $"{location} - {difficulty}";


        private bool IsHardDifficulty()
        {
            return difficulty == Common.Difficulty.Hard;
        }
    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
}