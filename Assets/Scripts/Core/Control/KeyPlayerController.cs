using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlayerController : PlayerController
{
    private void Update()
    {
        int horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        int vertical = (int)(Input.GetAxisRaw("Vertical"));

        if ((vertical != 0) || (horizontal != 0)){
            Direction direction = Direction.GetDirection2ValueFromCoord(horizontal, vertical);

            if (direction != Direction.IDLE) {
                TriggerMovement(direction);
            }           
        }       
    }
}
