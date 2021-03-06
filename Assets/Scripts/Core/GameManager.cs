﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    // time for basic action (e.g. moving)
    public float actionDuration = 0.1f;

    public bool playerCanMove = false;
    public bool isGamePaused = false;
    public bool isEndOfLevelScreen = false;
    public bool isGameOverScreen = false;

    #region Singleton

    //Awake is always called before any Start functions
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

        GameEvents.instance.onEndOfLevel += OnEndOfLevel;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEndOfLevel()
    {
        isEndOfLevelScreen = true;
    }

    private void Update()
    {
        if (isEndOfLevelScreen){
            if(GetAnyButArrowKeyDown()){
                isEndOfLevelScreen = false;
                // move to next scene
                LevelLoader.instance.LoadNextLevel();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            LevelLoader.instance.ReloadLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)){
            if (!isEndOfLevelScreen)
                TogglePause();
        }
    }

    public void Win()
    {
        Debug.Log("You win.");
        GameEvents.instance.EndOfLevelTrigger();
    }

    public void GameOver()
    {
        isGameOverScreen = true;
        BlockPlayer();
        GameEvents.instance.GameOverTrigger();
    }

    public void BlockPlayer(float unblockAfter = 0f)
    {
        playerCanMove = false;

        if (unblockAfter > 0f)
        {
            StartCoroutine(EnableMoveAfter(unblockAfter));
        }
    }

    private void TogglePause(){
        if (isGamePaused){
            ExitPause();
        }
        else{
            EnterPause();
        }
    }

    public void EnterPause(){
        isGamePaused = true;
        Chrono.instance.isCounting = false;
        InGameMenu menu = (InGameMenu) MainMenu.instance;
        menu.OpenMenu();
    }

    public void ExitPause(){
        isGamePaused = false;
        Chrono.instance.isCounting = true;
        InGameMenu menu = (InGameMenu) MainMenu.instance;
        menu.CloseMenu();
    }

    private IEnumerator EnableMoveAfter(float seconds) {

        yield return new WaitForSeconds(seconds);
        playerCanMove = true;
    }

    private static bool GetAnyButArrowKeyDown(){

        List<KeyCode> keyCodeList = new List<KeyCode>(){
            KeyCode.UpArrow,
            KeyCode.DownArrow,
            KeyCode.RightArrow,
            KeyCode.LeftArrow,
        };
        
        foreach(KeyCode keyCode in keyCodeList){
            if(Input.GetKeyDown(keyCode)){
                return false;
            }
        }

        return Input.anyKeyDown;
    }
}
