using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Tooltip("time for basic action (e.g. moving)")]
    public float actionDuration = 0.1f;
    public bool playerCanMove { get { return StateManager.instance.currentGameState == GameState.PLAY; } }

    #region Singleton

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

    private void Update()
    {
        switch (StateManager.instance.currentGameState.Name)
        {
            case "PLAY":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    EnterPause();
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    LevelLoader.instance.ReloadLevel();
                }
                break;
            case "PAUSE":
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ExitPause();
                }
                break;
            case "WIN":
                if (ControlUtils.GetAnyButArrowKeyDown())
                {
                    LevelLoader.instance.UnlockNextLevel();
                    LevelLoader.instance.SaveData();
                    LevelLoader.instance.LoadLevelSelectMenu();
                }
                break;
            default:
                break;
        }
    }

    public void Win()
    {
        Debug.Log("You win.");
        StateManager.instance.SetState(GameState.WIN);
    }

    public void GameOver()
    {
        StateManager.instance.SetState(GameState.GAME_OVER);
    }

    public void EnterPause()
    {
        StateManager.instance.SetState(GameState.PAUSE);
        Chrono.instance.isCounting = false;
        InGameMenu menu = (InGameMenu)MainMenu.instance;
        menu.OpenMenu();
    }

    public void ExitPause()
    {
        StateManager.instance.SetState(GameState.PLAY);
        Chrono.instance.isCounting = true;
        InGameMenu menu = (InGameMenu)MainMenu.instance;
        menu.CloseMenu();
    }
}
