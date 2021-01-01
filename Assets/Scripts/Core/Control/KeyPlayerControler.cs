using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlayerControler : PlayerControler
{
    private void Update()
    {
        int horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        int vertical = (int)(Input.GetAxisRaw("Vertical"));

        if ((vertical != 0) || (horizontal != 0)){
            Direction direction = new Direction(horizontal, vertical);

            if (!direction.IsIdle()) {
                TriggerMovement(direction);
            }           
        }       
    }
}
