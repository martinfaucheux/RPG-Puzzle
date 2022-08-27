using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : SingletoneBase<GameManager>
{
    [Tooltip("time for basic action (e.g. moving)")]
    public float actionDuration = 0.1f;
    public bool playerCanMove { get { return GameManager.allowMovementState.Contains(StateManager.instance.currentGameState); } }

    private static GameState[] forbiddenReloadState = new GameState[] {
        GameState.PAUSE, GameState.TRANSITION, GameState.LEVEL_SELECT
    };

    public static readonly GameState[] allowMovementState = new GameState[] {
        GameState.PLAY, GameState.LEVEL_SELECT
    };

    public void Restart()
    {
        if (!(forbiddenReloadState.Contains(StateManager.instance.currentGameState)))
            LevelLoader.instance.ReloadLevel();

    }

    public void Win()
    {
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

    public void ContinueToLevelSelect()
    {
        if (StateManager.instance.currentGameState == GameState.WIN)
        {
            List<int> unlockLevels = LevelLoader.instance.levelMetaData.unlockLevels;

            ProgressManager.instance.Save(); // Save progress of the current level
            SaveManager.instance.UnlockLevels(unlockLevels);
            LevelLoader.instance.LoadLevelSelectMenu();
        }
    }
}
