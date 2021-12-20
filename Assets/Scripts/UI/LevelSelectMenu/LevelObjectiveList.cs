using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelObjectiveList : MonoBehaviour
{
    public TextMeshProUGUI levelTitleTextComponent;
    public TextMeshProUGUI gemCountTextComponent;

    public TextMeshProUGUI[] questTextComponents;

    private int _selectedLevelId;

    void Start()
    {
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

        int gemCount = LevelLoader.instance.playerSavedData.GetCollectedGemCount(_selectedLevelId);
        return gemCount + " / " + levelMetaData.gemCount;
    }

    private void UpdateQuestTexts(LevelMetaData levelMetaData)
    {
        int questCount = 0;
        if (levelMetaData != null)
        {
            questCount = levelMetaData.quests.Count;
        }

        for (int questIndex = 0; questIndex < questTextComponents.Length; questIndex++)
        {
            TextMeshProUGUI textComponent = questTextComponents[questIndex];
            if (questIndex < questCount)
            {
                Quest quest = levelMetaData.quests[questIndex];
                textComponent.gameObject.SetActive(true);

                PlayerData playerData = LevelLoader.instance.playerSavedData;
                bool isQuestCompleted = playerData.IsQuestCompleted(_selectedLevelId, questIndex);

                string questText = quest.questName;
                if (isQuestCompleted)
                {
                    questText = "<s>" + questText + "</s>";
                }
                textComponent.text = questText;
            }
            else
            {
                textComponent.gameObject.SetActive(false);
            }
        }
    }
}
