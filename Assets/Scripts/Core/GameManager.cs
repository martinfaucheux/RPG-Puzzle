using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    // time for basic action (e.g. moving)
    public float actionDuration = 0.1f;

    public bool playerCanMove = false;
    public bool isGamePaused = false;

    public bool isLevelUpScreen = false;
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
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.onEnterLevelUp += OnEnterLevelUp;
        GameEvents.instance.onEndOfLevel += OnEndOfLevel;
    }

    private void OnEnterLevelUp()
    {
        isLevelUpScreen = true;
        playerCanMove = false;
    }

    private void OnEndOfLevel()
    {
        isEndOfLevelScreen = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelLoader.instance.ReloadLevel();
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (isEndOfLevelScreen)
            {
                isEndOfLevelScreen = false;

                // move to next scene
                LevelLoader.instance.LoadNextLevel();
            }

            else if (isGameOverScreen)
            {
                LevelLoader.instance.ReloadLevel();
            }
        }
    }

    public void ExitLevelUpScreen()
    {
        isLevelUpScreen = false;
        playerCanMove = true;

        // trigger end of level up event
        GameEvents.instance.LevelUpExitTrigger();
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
        Time.timeScale = 0f;
        InGameMenu menu = (InGameMenu) MainMenu.instance;
        menu.OpenMenu();
    }

    public void ExitPause(){
        isGamePaused = false;
        Time.timeScale = 1f;
        InGameMenu menu = (InGameMenu) MainMenu.instance;
        menu.CloseMenu();
    }

    private IEnumerator EnableMoveAfter(float seconds) {

        yield return new WaitForSeconds(seconds);
        playerCanMove = true;
    }
}
