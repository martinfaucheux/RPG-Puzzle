using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// <summary>
// Show the level related information
// used in both level selection and end-level recap
// </summary>
public class LevelObjectiveList : MonoBehaviour
{
    public TextMeshProUGUI levelTitleTextComponent;
    public TextMeshProUGUI gemCountTextComponent;

    public LevelObjectiveListElement[] objectiveElements;
    public TextMeshProUGUI questSeprator;
    private string _questSepratorText;

    private int _selectedLevelId;

    void Start()
    {
        _questSepratorText = questSeprator.text;
        UpdateUI();
    }

    public void SetLevel(int levelId)
    {
        _selectedLevelId = levelId;
        UpdateUI();
    }

    private void UpdateUI()
    {
        bool isLevelSelected = (_selectedLevelId > 0);
        LevelMetaData levelMetaData = null;

        if (isLevelSelected)
        {
            LevelMetaDataCollection levelCollection = LevelLoader.instance.levelCollection;
            levelMetaData = levelCollection.GetLevelBySceneBuildIndex(_selectedLevelId);

            gemCountTextComponent.text = GetGemCountString(levelMetaData);
        }
        if (levelTitleTextComponent != null)
            levelTitleTextComponent.text = GetLevelTitle();

        UpdateQuestTexts(levelMetaData);
    }

    private string GetLevelTitle()
    {
        if (_selectedLevelId <= 0)
        {
            return "";
        }
        return "Level " + _selectedLevelId.ToString();
    }

    private string GetGemCountString(LevelMetaData levelMetaData)
    {
        if (_selectedLevelId <= 0)
            return "";

        int gemCount = SaveManager.instance.GetCollectedGemCount(_selectedLevelId);
        return gemCount + " / " + levelMetaData.gemCount;
    }

    private void UpdateQuestTexts(LevelMetaData levelMetaData)
    {
        int questCount = 0;
        if (levelMetaData != null)
        {
            questCount = levelMetaData.quests.Count;
        }
        questSeprator.text = (questCount > 0) ? _questSepratorText : "";

        for (int questIndex = 0; questIndex < objectiveElements.Length; questIndex++)
        {
            LevelObjectiveListElement objectiveElement = objectiveElements[questIndex];
            if (questIndex < questCount)
            {
                Quest quest = levelMetaData.quests[questIndex];
                objectiveElement.gameObject.SetActive(true);

                bool isQuestCompleted = SaveManager.instance.IsQuestCompleted(_selectedLevelId, questIndex);
                bool isQuestNewlyCompleted = false;
                if (QuestManager.instance != null)
                {
                    isQuestNewlyCompleted = QuestManager.instance.IsNewlyCompleted(questIndex);
                }
                objectiveElement.SetContent(quest.questName, isQuestCompleted, isQuestNewlyCompleted);
            }
            else
            {
                objectiveElement.gameObject.SetActive(false);
            }
        }
    }
}
