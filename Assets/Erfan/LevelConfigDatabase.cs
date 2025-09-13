using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelConfigDatabase : Singleton<LevelConfigDatabase>
{
    public List<Common.GameTypeConfigPair> configPairs;

    private Dictionary<Common.GameType, LevelConfig> _configDictionary;

    public Dictionary<Common.GameType, LevelConfig> ConfigDictionary
    {
        get
        {
            if (_configDictionary == null)
            {
                _configDictionary = new Dictionary<Common.GameType, LevelConfig>();
                foreach (var pair in configPairs)
                {
                    _configDictionary[pair.gameType] = pair.config;
                }
            }
            return _configDictionary;
        }
    }
    
    [Button]
    public LevelConfig GetNextLevelConfig(LevelConfig currentConfig)
    {
        for (int i = 0; i < configPairs.Count; i++)
        {
            if (configPairs[i].config == currentConfig)
            {
                // Return the next one if it exists
                if (i + 1 < configPairs.Count)
                {
                    return configPairs[i + 1].config;
                }
                else
                {
                    // Reached the end
                    return null;
                }
            }
        }

        // Current config not found
        Debug.LogWarning("Current LevelConfig not found in configPairs.");
        return null;
    }
}
