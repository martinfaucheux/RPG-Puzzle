using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// TODO: rename to ProgressData

[System.Serializable]
public class PlayerData
{

    Dictionary<int, LevelProgressData> levelData;
    Dictionary<string, bool> seenInstructions;
    public int lastPlayedLevel { get; private set; } = -1;


    public PlayerData(
        Dictionary<int, LevelProgressData> levelData,
        Dictionary<string, bool> seenInstructions,
        int lastPlayedLevel = -1
    )
    {
        this.levelData = levelData;
        this.seenInstructions = seenInstructions;
        this.lastPlayedLevel = lastPlayedLevel;
    }

    public PlayerData() :
        this(
            // default PlayerData has first level unlocked
            new Dictionary<int, LevelProgressData>() { { 1, new LevelProgressData() } },
            new Dictionary<string, bool>()
        )
    { }

    public LevelProgressData this[int levelId]
    {
        get
        {
            if (levelId == 0)
                Debug.LogError("Attempt to fetch data of level 0");
            return levelData[levelId];
        }
        set => levelData[levelId] = value;
    }

    public void SetLastPlayedLevel(int levelId) => lastPlayedLevel = levelId;

    public void Unlock(int levelId)
    {
        if (!IsUnlocked(levelId))
            levelData[levelId] = new LevelProgressData();
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
        return levelData[levelId].GetCollectedGemsCount();
    }

    public int GetCompletedQuestsCount(int levelId)
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

    public void AddSeenInstruction(string instructionName)
    {
        if (!seenInstructions.ContainsKey(instructionName))
        {
            seenInstructions[instructionName] = true;
        }
    }

    public bool HasSeenInstruction(string instructionName) => seenInstructions.ContainsKey(instructionName);
}

