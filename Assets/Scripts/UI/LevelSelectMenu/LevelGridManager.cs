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
    public LevelObjectiveList levelObjectiveList;

    public Color defaultButtonColor;
    public Color defaultReflectionColor;
    public Color allGemsCollectedColor;
    public Color allQuestsCollectedColor;

    private int _selectedLevelId = 0;

    void Start()
    {
        LevelLoader.instance.RetrieveGameState();
        InstantiateLevelButtons();
        UpdateUI();
    }

    public void SelectLevel(int levelId)
    {
        levelObjectiveList.SetLevel(levelId);

        _selectedLevelId = levelId;
        UpdateUI();
    }

    public void LoadSelectedLevel()
    {
        LevelLoader.instance.LoadLevel(_selectedLevelId);
    }

    private void UpdateUI()
    {
        // show / hide content of side menu
        bool isLevelSelected = (_selectedLevelId > 0);
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

            Color backgroundColor = defaultButtonColor;
            Color reflectionColor = defaultReflectionColor;

            int collectedGems = LevelLoader.instance.playerSavedData.GetCollectedGemCount(levelId);
            int completedQuests = LevelLoader.instance.playerSavedData.GetCompletedQuestsCount(levelId);
            if (collectedGems == levelMeta.gemCount)
            {
                if (completedQuests == levelMeta.quests.Count)
                {
                    backgroundColor = allQuestsCollectedColor;
                    reflectionColor = Color.white;
                }
                else
                {
                    backgroundColor = allGemsCollectedColor;
                    reflectionColor = Color.white;
                }
            }
            LevelSelectButton selectButton = newGO.GetComponent<LevelSelectButton>();
            selectButton.levelId = levelId;
            selectButton.SetBackgroundColor(backgroundColor);
            selectButton.SetReflectionColor(reflectionColor);

            selectButton.SetButtonActive(LevelLoader.instance.IsLevelUnlocked(levelId));
        }
    }
}
