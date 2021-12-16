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

    public LevelSaveData this[int levelId]
    {
        get => levelData[levelId];
        set => levelData[levelId] = value;
    }

    public void Update(int levelId, bool[] gemsCollected, bool[] questCompleted)
    {
        if (IsUnlocked(levelId))
        {
            levelData[levelId].Update(gemsCollected, questCompleted);
        }
        else
        {
            levelData[levelId] = new LevelSaveData(gemsCollected, questCompleted);
        }
    }

    public void Unlock(int levelId)
    {
        if (!IsUnlocked(levelId))
            levelData[levelId] = new LevelSaveData();
    }

    public void AddGem(int levelId, int gemId)
    {
        levelData[levelId].gemsCollected[gemId] = true;
    }

    public bool IsUnlocked(int levelId)
    {
        return levelData.ContainsKey(levelId);
    }

    public List<int> GetUnlockedLevels()
    {
        return levelData.Keys.ToList();
    }

    public int GetCollectedGemCount(int levelId)
    {
        if (!IsUnlocked(levelId))
        {
            return 0;
        }
        return levelData[levelId].gemsCollected.Where(x => x).Count();
    }

    public int GetCompletedQuestsCount(int levelId)
    {
        if (!IsUnlocked(levelId))
        {
            return 0;
        }
        return levelData[levelId].questsCompleted.Where(x => x).Count();
    }

    public bool IsGemCollected(int levelId, int gemId)
    {
        if (!IsUnlocked(levelId))
        {
            return false;
        }
        return levelData[levelId].gemsCollected[gemId];
    }

    public bool IsQuestCompleted(int levelId, int questId)
    {
        if (!IsUnlocked(levelId))
        {
            return false;
        }
        return levelData[levelId].questsCompleted[questId];
    }
}

[System.Serializable]
public class LevelSaveData
{
    public bool[] gemsCollected;
    public bool[] questsCompleted;

    public LevelSaveData(bool[] gemsCollected, bool[] questsCompleted)
    {
        this.gemsCollected = gemsCollected;
        this.questsCompleted = questsCompleted;
    }

    public LevelSaveData() : this(
        new bool[] { false }, new bool[] { false }
    )
    { }

    public void Update(bool[] gemsCollected, bool[] questsCompleted)
    {
        for (int gemIndex = 0; gemIndex < gemsCollected.Length; gemIndex++)
        {
            this.gemsCollected[gemIndex] |= gemsCollected[gemIndex];
        }

        for (int questIndex = 0; questIndex < questsCompleted.Length; questIndex++)
        {
            this.questsCompleted[questIndex] |= questsCompleted[questIndex];
        }
    }
}