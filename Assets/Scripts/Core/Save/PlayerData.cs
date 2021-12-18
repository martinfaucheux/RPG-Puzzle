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

    public void Unlock(int levelId)
    {
        if (!IsUnlocked(levelId))
            levelData[levelId] = new LevelSaveData();
    }

    public void AddGem(int levelId, int gemId)
    {
        Unlock(levelId);
        levelData[levelId].AddGem(gemId);
    }

    public void AddQuest(int levelId, int questId)
    {
        Unlock(levelId);
        levelData[levelId].AddQuest(questId);
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
        return levelData[levelId].GetQuestCompleteCount();
    }


    public bool IsGemCollected(int levelId, int gemId)
    {
        if (!IsUnlocked(levelId))
        {
            return false;
        }
        return levelData[levelId].isGemCollected(gemId);
    }

    public bool IsQuestCompleted(int levelId, int questId)
    {
        if (!IsUnlocked(levelId))
        {
            return false;
        }
        return levelData[levelId].isQuestCompleted(questId);
    }
}

[System.Serializable]
public class LevelSaveData
{
    private Dictionary<int, bool> gemsCollected;
    private Dictionary<int, bool> questsCompleted;
    public LevelSaveData(bool[] gemsCollected, bool[] questsCompleted)
    {
        this.gemsCollected = new Dictionary<int, bool>();
        this.questsCompleted = new Dictionary<int, bool>();

        for (int i = 0; i < gemsCollected.Length; i++)
        {
            this.gemsCollected[i] = gemsCollected[i];
        }

        for (int i = 0; i < questsCompleted.Length; i++)
        {
            this.questsCompleted[i] = questsCompleted[i];
        }
    }

    public LevelSaveData()
    {
        this.gemsCollected = new Dictionary<int, bool>();
        this.questsCompleted = new Dictionary<int, bool>();
    }

    public bool isQuestCompleted(int questId)
    {
        if (questsCompleted.ContainsKey(questId))
        {
            return questsCompleted[questId];
        }
        return false;
    }

    public bool isGemCollected(int gemId)
    {
        if (gemsCollected.ContainsKey(gemId))
        {
            return gemsCollected[gemId];
        }
        return false;
    }

    public void AddGem(int gemId)
    {
        gemsCollected[gemId] = true;
    }

    public void AddQuest(int questId)
    {
        questsCompleted[questId] = true;
    }

    public int GetQuestCompleteCount()
    {
        return gemsCollected.Where(kv => kv.Value).Count();
    }

}