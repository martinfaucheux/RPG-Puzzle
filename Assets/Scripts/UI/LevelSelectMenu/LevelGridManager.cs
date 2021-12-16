using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelGridManager : MonoBehaviour
{

    public GameObject levelButtonPrefab;

    public Transform gridContainerTransform;

    public Transform[] noSelectHiddenTransforms;

    public TextMeshProUGUI levelTitleTextComponent;
    public TextMeshProUGUI gemCountTextComponent;

    public TextMeshProUGUI[] questTextComponents;

    private int _selectedLevelId = 0;

    void Start()
    {
        LevelLoader.instance.RetrieveGameState();
        InstantiateLevelButtons();
        UpdateUI();
    }

    public void SelectLevel(int levelId)
    {
        _selectedLevelId = levelId;
        UpdateUI();
    }

    public void LoadSelectedLevel()
    {
        LevelLoader.instance.LoadLevel(_selectedLevelId);
    }

    private void UpdateUI()
    {
        bool isLevelSelected = (_selectedLevelId > 0);
        LevelMetaData levelMetaData = null;

        if (isLevelSelected)
        {
            LevelMetaDataCollection levelCollection = LevelLoader.instance.levelCollection;
            levelMetaData = levelCollection.GetLevelBySceneBuildIndex(_selectedLevelId);

            levelTitleTextComponent.text = GetLevelTitle();
            gemCountTextComponent.text = GetGemCountString(levelMetaData);
        }
        UpdateQuestTexts(levelMetaData);

        // show / hide content of side menu
        foreach (Transform transformToHide in noSelectHiddenTransforms)
        {
            transformToHide.gameObject.SetActive(isLevelSelected);
        }
    }

    private void InstantiateLevelButtons()
    {
        foreach (LevelMetaData levelMeta in LevelLoader.instance.levelCollection.levelList)
        {
            int levelId = levelMeta.sceneBuildIndex;
            GameObject newGO = Instantiate(levelButtonPrefab, gridContainerTransform);
            newGO.GetComponent<LevelSelectButton>().levelId = levelId;
            newGO.GetComponent<Button>().interactable = LevelLoader.instance.IsLevelUnlocked(levelId);
        }
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

    private string GetQuestString(LevelMetaData levelMetaData)
    {
        if (_selectedLevelId <= 0 || levelMetaData.quests.Count == 0)
            return "";

        int questCount = LevelLoader.instance.playerSavedData.GetCompletedQuestsCount(_selectedLevelId);
        return questCount + " / " + levelMetaData.quests.Count;
    }
}
