using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGridManager : MonoBehaviour
{

    public GameObject levelButtonPrefab;

    public Transform gridContainerTransform;

    public Transform[] noSelectHiddenTransforms;

    public TextMeshProUGUI levelTitleTextComponent;
    public TextMeshProUGUI gemCountTextComponent;

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
        levelTitleTextComponent.text = GetLevelTitle();
        gemCountTextComponent.text = GetGemCountString();

        // show / hide content of side menu
        foreach (Transform transformToHide in noSelectHiddenTransforms)
        {
            transformToHide.gameObject.SetActive(isLevelSelected);
        }
    }

    private void InstantiateLevelButtons()
    {
        foreach (int levelId in LevelLoader.instance.GetUnlockedLevels())
        {
            GameObject newGO = Instantiate(levelButtonPrefab, gridContainerTransform);
            newGO.GetComponent<LevelSelectButton>().levelId = levelId;
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

    private string GetGemCountString()
    {
        if (_selectedLevelId <= 0)
            return "";

        LevelMetaDataCollection levelCollection = LevelLoader.instance.levelCollection;
        LevelMetaData levelMetaData = levelCollection.GetLevelBySceneBuildIndex(_selectedLevelId);

        int gemCount = LevelLoader.instance.playerSavedData.GetCollectedGemCount(_selectedLevelId);
        return gemCount + " / " + levelMetaData.gemCount;
    }
}
