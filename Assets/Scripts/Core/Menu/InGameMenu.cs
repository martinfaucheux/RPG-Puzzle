using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : BaseMenu
{
    private bool _isOpen = false;
    public GameObject nextLevelButtonGO;
    public GameObject previousLevelButtonGO;
    public Text titleTextComponent;

    public override void Start(){

        base.Start();

        // retrieve data from player.data
        LevelLoader.instance.RetrieveGameState();
    }

    public void TriggerMenu(){
        if (_isOpen){
            CloseMenu();
        }
        else{
            OpenMenu();
        }
    }

    public void OpenMenu(){
        SetTittle();
        SetPreviousLevelButtonState();
        SetNextLevelButtonState();

        mainMenuGO.SetActive(true);
        backgroundGO.SetActive(true);
        _isOpen = true;
    }

    public void CloseMenu(){
        mainMenuGO.SetActive(false);
        optionMenuGO.SetActive(false);
        backgroundGO.SetActive(false);
        _isOpen = false;
    }

    private void SetTittle(){
        string text = "Level " + LevelLoader.instance.currentLevelId.ToString();
        titleTextComponent.text = text;
    }

    private void SetPreviousLevelButtonState(){
        bool hasNextLevel = LevelLoader.instance.IsPreviousLevelAvailable();
        previousLevelButtonGO.SetActive(hasNextLevel);
    }

    private void SetNextLevelButtonState(){
        bool hasNextLevel = LevelLoader.instance.IsNextLevelAvailable();
        nextLevelButtonGO.SetActive(hasNextLevel);
    }
}
