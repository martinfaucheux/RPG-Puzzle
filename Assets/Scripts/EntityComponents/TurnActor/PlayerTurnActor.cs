using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnActor : TurnActor
{
    private Direction nextCommand;

    public void SetCommand(Direction command) => nextCommand = command;

    public override IEnumerator DoTurn()
    {
        yield return StartCoroutine(movingObject.AttemptMove(nextCommand));
        nextCommand = Direction.IDLE;
    }

}
