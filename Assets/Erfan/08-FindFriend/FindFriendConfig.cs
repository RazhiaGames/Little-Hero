using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "FindFriendConfig", menuName = "Erfan/Scriptable Objects/FindFriendConfig")]
public class FindFriendConfig : LevelConfig
{
    [ListDrawerSettings()]
    public List<ZoneDifficultyConfig> configurations = new List<ZoneDifficultyConfig>();

    [System.Serializable]
    public class ZoneDifficultyConfig
    {
        [FoldoutGroup("$GroupName", false)] public Common.Location location;
        [FoldoutGroup("$GroupName")] public Common.Difficulty difficulty;
        [FoldoutGroup("$GroupName")] public Sprite sampleFriendPic;
        [FoldoutGroup("$GroupName")] public Friend sampleFriend;
        [FoldoutGroup("$GroupName")] public List<Friend> Friends = new List<Friend>();
        [GUIColor("cyan"), FoldoutGroup("$GroupName")] public string howToPlayText;
        [FoldoutGroup("$GroupName")]public AudioClip howToPlayAudio;


        private string GroupName => $"{location} - {difficulty}";
    }

    public ZoneDifficultyConfig GetConfig(Common.Location zone, Common.Difficulty difficulty)
    {
        return configurations.FirstOrDefault(cfg => cfg.location == zone && cfg.difficulty == difficulty);
    }
    
    [Serializable]
    public class Friend
    {
        public string mName;
        public FriendType FriendType;
        public FriendEmotion FriendEmotion;
    }

    public enum FriendType
    {
        boy,
        girl,
        cat,
        dog,
        scarf,
        tShirt,
        shoe,
        pants
    }
    
    public enum FriendEmotion
    {
        Scared,
        Happy,
        Angry,
        Sad,
        Red,
        Green,
        Blue,
        White,
        Diamond,
        Wool,
        Black,
        None
    }
}