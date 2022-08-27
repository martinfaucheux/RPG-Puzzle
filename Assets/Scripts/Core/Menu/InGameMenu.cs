using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MainMenu
{
    private bool _isOpen = false;
    public GameObject nextLevelButtonGO;
    public GameObject previousLevelButtonGO;
    public Text titleTextComponent;

    public override void Start()
    {

        base.Start();
    }

    public void TriggerMenu()
    {
        if (_isOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    public void OpenMenu()
    {
        SetTittle();
        SetPreviousLevelButtonState();
        SetNextLevelButtonState();

        mainMenuGO.SetActive(true);
        backgroundGO.SetActive(true);
        _isOpen = true;
    }

    public void CloseMenu()
    {
        mainMenuGO.SetActive(false);
        backgroundGO.SetActive(false);
        _isOpen = false;

        if (optionMenuGO != null)
            optionMenuGO?.SetActive(false);

    }

    private void SetTittle()
    {
        string text = "Level " + LevelLoader.instance.currentLevelId.ToString();
        titleTextComponent.text = text;
    }

    private void SetPreviousLevelButtonState()
    {
        if (previousLevelButtonGO != null)
        {
            bool hasNextLevel = SaveManager.instance.IsLevelUnlocked(
                LevelLoader.instance.currentLevelId - 1
            );
            previousLevelButtonGO.SetActive(hasNextLevel);
        }
    }

    private void SetNextLevelButtonState()
    {
        if (nextLevelButtonGO)
        {
            bool hasNextLevel = SaveManager.instance.IsLevelUnlocked(
                LevelLoader.instance.currentLevelId + 1
            );
            nextLevelButtonGO.SetActive(hasNextLevel);
        }
    }
}
