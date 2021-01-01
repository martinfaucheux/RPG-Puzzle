using System;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public static event Action<Direction> OnGetCommand = delegate { };

    protected void TriggerMovement(Direction direction)
    {
        OnGetCommand(direction);
    }
}
