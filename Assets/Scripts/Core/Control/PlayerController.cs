using System;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public static event Action<Direction2> OnGetCommand = delegate { };

    protected void TriggerMovement(Direction2 direction)
    {
        OnGetCommand(direction);
    }
}
