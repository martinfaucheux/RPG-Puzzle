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
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        // TODO: all the logic to check if player is able to move should be in GameManager instead
        if (
            IsReady()
            && GameManager.instance.playerCanMove
            && !GameManager.instance.isGamePaused
            && !MenuController.instance.isOpen
        ){

            Direction direction = new Direction(horizontal, vertical);

            if (!direction.IsIdle()) {
                AttemptMove(direction);
            }
        }       
    }
    
    


}
