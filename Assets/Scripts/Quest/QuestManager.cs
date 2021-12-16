using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<Quest> quests { get { return LevelLoader.instance.levelMetaData.quests; } }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Quest quest in quests)
        {
            quest.Initialize();
        }

        GameEvents.instance.onEndOfLevel += CheckCompletion;
    }

    void OnDestroy()
    {
        GameEvents.instance.onEndOfLevel -= CheckCompletion;
    }

    public void CheckCompletion()
    {
        PlayerData savedPlayerData = LevelLoader.instance.playerSavedData;
        int levelId = LevelLoader.instance.currentLevelId;
        bool[] completedQuests = savedPlayerData[levelId].questsCompleted;

        for (int questIndex = 0; questIndex < quests.Count; questIndex++)
        {
            Quest quest = quests[questIndex];
            bool isComplete = quest.CheckCompletion();

            completedQuests[questIndex] |= isComplete;

            string str = "Quest '{0}' completed: {1}";
            Debug.Log(string.Format(str, quest.name, isComplete));
        }
        DataSaver.SaveGameState(savedPlayerData);
    }
}