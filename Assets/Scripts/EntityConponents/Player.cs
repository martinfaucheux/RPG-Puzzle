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

    private void TriggerMovement(Direction direction){
        if (
            IsReady()
            && GameManager.instance.playerCanMove
            && !GameManager.instance.isGamePaused
            && !MenuController.instance.isOpen
            && !direction.IsIdle()
        ){
            AttemptMove(direction);
        }       
    }

}
