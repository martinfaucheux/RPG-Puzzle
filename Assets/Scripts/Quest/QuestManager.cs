using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : SingletoneBase<QuestManager>
{
    private List<Quest> _quests;
    private List<bool> _initQuestState;
    private int _levelId { get => LevelLoader.instance.currentLevelId; }

    void Start()
    {
        _quests = new List<Quest>();
        _initQuestState = new List<bool>();
        if (LevelLoader.instance.levelMetaData != null)
        {
            foreach (Quest scriptableObject in LevelLoader.instance.levelMetaData.quests)
            {
                Quest quest = Instantiate(scriptableObject);
                // Quest quest = (Quest)ScriptableObject.CreateInstance(scriptableObject.GetType());
                quest.Initialize();
                _quests.Add(quest);
            }
        }
        else
            Debug.LogWarning("No level Metadata found");

        InitializeQuestState();
        GameEvents.instance.onEnterState += OnEnterState;
    }

    void OnDestroy()
    {
        GameEvents.instance.onEnterState -= OnEnterState;
    }

    private void OnEnterState(GameState state)
    {
        if (state == GameState.WIN)
        {
            CheckCompletion();
        }
    }

    public bool IsNewlyCompleted(int questId)
    {
        return (
             SaveManager.instance.IsQuestCompleted(_levelId, questId)
            && !_initQuestState[questId]
        );
    }

    private void InitializeQuestState()
    {
        for (int questId = 0; questId < _quests.Count; questId++)
        {
            bool IsQuestCompleted = SaveManager.instance.IsQuestCompleted(_levelId, questId);
            _initQuestState.Add(IsQuestCompleted);
        }
    }

    private void CheckCompletion()
    {
        for (int questIndex = 0; questIndex < _quests.Count; questIndex++)
        {
            Quest quest = _quests[questIndex];
            bool isComplete = quest.CheckCompletion();

            if (isComplete)
                ProgressManager.instance.AddQuest(questIndex);

            string str = "Quest '{0}' completed: {1}";
            Debug.Log(string.Format(str, quest.name, isComplete));
        }
    }
}
