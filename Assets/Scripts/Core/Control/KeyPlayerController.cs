using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlayerController : PlayerController
{
    private bool _rotateControl = false;
    private void Start(){
        CollisionMatrix _collisionMatrix = CollisionMatrix.instance;
        if (_collisionMatrix != null){
            _rotateControl = (_collisionMatrix.mode == CollisionMatrix.Mode.ISOMETRIC);
        }
    }
    private void Update()
    {
        int horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        int vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (_rotateControl){
            int tmpVert = vertical;
            vertical = -horizontal;
            horizontal = tmpVert; 
        }

        if ((vertical != 0) || (horizontal != 0)){
            Direction direction = Direction.GetDirection2ValueFromCoord(horizontal, vertical);

            if (direction != Direction.IDLE) {
                TriggerMovement(direction);
            }           
        }       
    }
}
