using System.Collections.Generic;
using System.Linq;


[System.Serializable]
public class LevelProgressData
{
    private Dictionary<int, bool> gemsCollected;
    private Dictionary<int, bool> questsCompleted;

    // Constructor with dicts
    public LevelProgressData(
        Dictionary<int, bool> gemsCollected,
        Dictionary<int, bool> questsCompleted
    )
    {
        this.gemsCollected = gemsCollected;
        this.questsCompleted = questsCompleted;
    }

    // Constructor with bool arrays
    public LevelProgressData(bool[] gemsCollected, bool[] questsCompleted)
    {
        this.gemsCollected = new Dictionary<int, bool>();
        this.questsCompleted = new Dictionary<int, bool>();

        for (int i = 0; i < gemsCollected.Length; i++)
            this.gemsCollected[i] = gemsCollected[i];

        for (int i = 0; i < questsCompleted.Length; i++)
            this.questsCompleted[i] = questsCompleted[i];
    }

    // default constructor
    public LevelProgressData()
    {
        this.gemsCollected = new Dictionary<int, bool>();
        this.questsCompleted = new Dictionary<int, bool>();
    }

    public LevelProgressData Merge(LevelProgressData otherData)
    {
        Dictionary<int, bool> _gemsCollected = new Dictionary<int, bool>(this.gemsCollected);
        Dictionary<int, bool> _questsCompleted = new Dictionary<int, bool>(this.questsCompleted);

        foreach (KeyValuePair<int, bool> kvp in otherData.gemsCollected)
        {
            int gemId = kvp.Key;
            bool isCollected = kvp.Value;
            if (_gemsCollected.ContainsKey(gemId))
                _gemsCollected[gemId] |= isCollected;
            else
                _gemsCollected[gemId] = isCollected;
        }

        foreach (KeyValuePair<int, bool> kvp in otherData.questsCompleted)
        {
            int questId = kvp.Key;
            bool isCompleted = kvp.Value;
            if (_questsCompleted.ContainsKey(questId))
                _questsCompleted[questId] |= isCompleted;
            else
                _questsCompleted[questId] = isCompleted;
        }

        return new LevelProgressData(_gemsCollected, _questsCompleted);
    }

    public bool isQuestCompleted(int questId)
    {
        if (questsCompleted.ContainsKey(questId))
            return questsCompleted[questId];
        return false;
    }

    public bool isGemCollected(int gemId)
    {
        if (gemsCollected.ContainsKey(gemId))
            return gemsCollected[gemId];

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
        return questsCompleted.Where(kv => kv.Value).Count();
    }

    public int GetCollectedGemsCount()
    {
        return gemsCollected.Where(kv => kv.Value).Count();
    }

}