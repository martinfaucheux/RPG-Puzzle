using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenTurnActor : TurnActor
{
    [SerializeField] MovingObject movingObject;

    public override IEnumerator DoTurn()
    {
        Direction direction = movingObject.faceDirection;
        // try to move toward face direction
        if (movingObject.IsDirectionAllowed(direction))
        {
            yield return StartCoroutine(movingObject.AttemptMove(direction));
        }
        // else try opposite direction
        else if (movingObject.IsDirectionAllowed(direction.Opposite()))
        {
            yield return StartCoroutine(movingObject.AttemptMove(direction.Opposite()));
        }
        else
        {
            yield break;
        }
    }

}
