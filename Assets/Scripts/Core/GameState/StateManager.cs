using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : SingletoneBase<StateManager>
{
    public GameState currentGameState { get; private set; } = GameState.TRANSITION;
    public GameState previousGameState { get; private set; } = GameState.TRANSITION;

    public void SetState(GameState gameState)
    {
        previousGameState = currentGameState;
        currentGameState = gameState;

        GameEvents.instance.ExitStateTrigger(previousGameState);
        GameEvents.instance.EnterStateTrigger(currentGameState);
    }

}
