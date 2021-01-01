using System;
using UnityEngine;

public abstract class PlayerControler : MonoBehaviour
{
    public static event Action<Direction> OnGetCommand = delegate { };

    protected void TriggerMovement(Direction direction)
    {
        OnGetCommand(direction);
    }
}
