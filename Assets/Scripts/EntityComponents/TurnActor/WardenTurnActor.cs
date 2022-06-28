using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenTurnActor : TurnActor
{
    public override IEnumerator DoTurn()
    {
        Direction direction = movingObject.faceDirection;
        // try to move toward face direction
        if (movingObject.IsInteractionAllowed(direction))
        {
            yield return StartCoroutine(movingObject.AttemptMove(direction));
        }
        // else try opposite direction
        else if (movingObject.IsInteractionAllowed(direction.Opposite()))
        {
            yield return StartCoroutine(movingObject.AttemptMove(direction.Opposite()));
        }
        else
        {
            yield break;
        }
    }

}
