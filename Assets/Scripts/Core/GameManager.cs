using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Tooltip("time for basic action (e.g. moving)")]
    public float actionDuration = 0.1f;
    public bool playerCanMove { get { return StateManager.instance.currentGameState == GameState.PLAY; } }

    private static GameState[] forbiddenReloadState = new GameState[] {
        GameState.PAUSE, GameState.TRANSITION
    };


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

    public void Restart()
    {
        if (!(forbiddenReloadState.Contains(StateManager.instance.currentGameState)))
        {
            LevelLoader.instance.ReloadLevel();
        }
    }

    public void Win()
    {
        Debug.Log("You win.");
        StateManager.instance.SetState(GameState.WIN);
    }

    public void GameOver()
    {
        TurnManager.instance.StopTurn();
        StateManager.instance.SetState(GameState.GAME_OVER);
    }

    public void EnterPause()
    {
        if (StateManager.instance.currentGameState == GameState.PLAY)
        {
            StateManager.instance.SetState(GameState.PAUSE);
            PlayInputManager.instance.SwitchCurrentActionMap("UI");
            Chrono.instance.isCounting = false;
            InGameMenu menu = (InGameMenu)MainMenu.instance;
            menu.OpenMenu();
        }
    }

    public void ExitPause()
    {
        if (StateManager.instance.currentGameState == GameState.PAUSE)
        {
            StateManager.instance.SetState(GameState.PLAY);
            PlayInputManager.instance.SwitchCurrentActionMap("Player");
            Chrono.instance.isCounting = true;
            InGameMenu menu = (InGameMenu)MainMenu.instance;
            menu.CloseMenu();
        }
    }

    public void EnterPostWin()
    {
        if (StateManager.instance.currentGameState == GameState.WIN)
        {
            LevelLoader.instance.UnlockNextLevel();
            LevelLoader.instance.SaveData();
            LevelLoader.instance.LoadLevelSelectMenu();
        }
    }
}
