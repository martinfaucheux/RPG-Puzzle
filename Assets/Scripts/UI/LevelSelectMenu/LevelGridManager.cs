using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGridManager : MonoBehaviour
{

    public GameObject levelButtonPrefab;

    public Transform playButtonTransform;

    public TextMeshProUGUI levelTitleTextComponent;

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
        playButtonTransform.gameObject.SetActive(isLevelSelected);
        levelTitleTextComponent.text = GetLevelTitle();
    }

    private void InstantiateLevelButtons()
    {
        int startIndex = Mathf.Max(1, transform.childCount);
        for (int i = startIndex; i < LevelLoader.instance.maxLevelId; i++)
        {
            Instantiate(levelButtonPrefab, transform);
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
}
