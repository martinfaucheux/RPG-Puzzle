using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    private List<Quest> _quests;

    private List<bool> _initQuestState;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a CollisionMatrix.
            Destroy(gameObject);
    }

    void Start()
    {
        _quests = new List<Quest>();
        _initQuestState = new List<bool>();
        foreach (Quest scriptableObject in LevelLoader.instance.levelMetaData.quests)
        {
            Quest quest = Instantiate(scriptableObject);
            // Quest quest = (Quest)ScriptableObject.CreateInstance(scriptableObject.GetType());
            quest.Initialize();
            _quests.Add(quest);
        }
        InitializeQuestState();
        GameEvents.instance.onEndOfLevel += CheckCompletion;
    }

    void OnDestroy()
    {
        GameEvents.instance.onEndOfLevel -= CheckCompletion;
    }

    public bool IsNewlyCompleted(int questId)
    {
        return (
            LevelLoader.instance.playerSavedData.IsQuestCompleted(LevelLoader.instance.currentLevelId, questId)
            && !_initQuestState[questId]
        );
    }

    private void InitializeQuestState()
    {
        for (int questId = 0; questId < _quests.Count; questId++)
        {
            _initQuestState.Add(LevelLoader.instance.playerSavedData.IsQuestCompleted(LevelLoader.instance.currentLevelId, questId));
        }
    }

    private void CheckCompletion()
    {
        PlayerData savedPlayerData = LevelLoader.instance.playerSavedData;
        int levelId = LevelLoader.instance.currentLevelId;

        for (int questIndex = 0; questIndex < _quests.Count; questIndex++)
        {
            Quest quest = _quests[questIndex];
            bool isComplete = quest.CheckCompletion();

            if (isComplete)
            {
                savedPlayerData[levelId].AddQuest(questIndex);
            }

            string str = "Quest '{0}' completed: {1}";
            Debug.Log(string.Format(str, quest.name, isComplete));
        }
        LevelLoader.instance.SaveData();
    }
}
