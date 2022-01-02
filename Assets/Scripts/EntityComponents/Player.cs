using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MovingObject
{

    public static Player instance;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a CollisionMatrix.
            Destroy(gameObject);

        PlayerController.OnGetCommand += TriggerMovement;
    }

    private void OnDestroy()
    {
        PlayerController.OnGetCommand -= TriggerMovement;
    }

    private void TriggerMovement(Direction direction)
    {
        if (
            IsReady()
            && GameManager.instance.playerCanMove
            && !GameManager.instance.isEndOfLevelScreen
            && !GameManager.instance.isGamePaused
            && !GameManager.instance.isInstruction
            && !MenuController.instance.isOpen
            && !SkillMenu.instance.isShowing
            && (direction != Direction.IDLE)
        )
        {
            AttemptMove(direction);
        }
    }

    protected override bool Move(Direction direction)
    {
        bool hasMoved = base.Move(direction);

        // Vector2Int position = _matrixCollider.matrixPosition += direction.ToPos();

        // player has already moved at this moment
        GameEvents.instance.PlayerMoveTrigger(_matrixCollider.matrixPosition);
        return hasMoved;
    }

}
