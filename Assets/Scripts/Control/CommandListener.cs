using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandListener : MonoBehaviour
{

    [field: SerializeField]
    public bool allowCommandIfNotReady { get; private set; } = true;

    public Direction nextCommand { get; private set; } = Direction.IDLE;

    private TurnManager _turnManager { get { return TurnManager.instance; } }

    void Start()
    {
        PlayInputManager.OnGetCommand += SetNextCommand;
    }

    void OnDestroy()
    {
        PlayInputManager.OnGetCommand -= SetNextCommand;
    }

    public void SetNextCommand(Direction command)
    {
        if (
            (allowCommandIfNotReady || _turnManager.isReady)
            && nextCommand == Direction.IDLE
        )
            nextCommand = command;
    }

    void Update()
    {
        if (
            _turnManager.isReady
            && nextCommand != Direction.IDLE
            && GameManager.instance.playerCanMove
        )
        {
            // turn is triggered only if player interaction is allowed
            // e.g. not moving to a wall
            if (_turnManager.playerTurnActor.movingObject.IsInteractionAllowed(nextCommand))
            {
                _turnManager.playerTurnActor.SetCommand(nextCommand);
                _turnManager.DoTurn();
            }
            // reset next command
            nextCommand = Direction.IDLE;
        }
    }
}
