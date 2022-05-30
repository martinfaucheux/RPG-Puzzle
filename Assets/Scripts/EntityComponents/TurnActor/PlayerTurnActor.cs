using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnActor : TurnActor
{
    [SerializeField] MovingObject movingObject;
    private Direction nextCommand;

    protected override void Start()
    {
        base.Start();
        PlayerController.OnGetCommand += TriggerMovement;
    }

    protected override void OnDestroy()
    {
        PlayerController.OnGetCommand -= TriggerMovement;
        base.OnDestroy();
    }

    private void TriggerMovement(Direction direction)
    {
        if (
            TurnManager.instance.isReady
            & GameManager.instance.playerCanMove
            & !GameManager.instance.isEndOfLevelScreen
            & !GameManager.instance.isGamePaused
            & !GameManager.instance.isInstruction
            & !MenuController.instance.isOpen
            & !SkillMenu.instance.isShowing
            & (direction != Direction.IDLE)
        )
        {
            // Set the player command for the upcoming turn
            SetCommand(direction);

            // Start turn
            TurnManager.instance.DoTurn();
        }
    }

    public void SetCommand(Direction direction)
    {
        nextCommand = direction;
    }

    public override IEnumerator DoTurn()
    {
        yield return StartCoroutine(movingObject.AttemptMove(nextCommand));
        nextCommand = Direction.IDLE;
    }

}
