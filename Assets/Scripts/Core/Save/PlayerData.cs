using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class PlayerData
{

    Dictionary<int, LevelSaveData> levelData;

    // public int maxUnlockedLevel
    // {
    //     get; private set;
    // }

    public PlayerData(Dictionary<int, LevelSaveData> levelData)
    {
        this.levelData = levelData;
        // maxUnlockedLevel = levelData.Keys.Max();
    }

    public PlayerData() :
        this(new Dictionary<int, LevelSaveData>(){
            // default PlayerData has first level unlocked
            {1, new LevelSaveData()}
        })
    { }

    public void Update(int levelId, LevelSaveData levelSaveData)
    {
        levelData[levelId] = levelSaveData;
    }

    public bool IsUnlocked(int levelId)
    {
        return levelData.ContainsKey(levelId);
    }

    public List<int> GetUnlockedLevels()
    {
        return levelData.Keys.ToList();
    }
}

[System.Serializable]
public class LevelSaveData
{
    int collectedGems;
    bool isSideQuestCompleted;

    public LevelSaveData(int collectedGems, bool isSideQuestCompleted)
    {
        this.collectedGems = collectedGems;
        this.isSideQuestCompleted = isSideQuestCompleted;
    }

    public LevelSaveData() : this(0, false) { }
}