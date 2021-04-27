using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : ActivableObject
{
    private bool isWalkable = true;

    public Sprite activatedSprite;

    // return true if source object can move after activation
    public override bool Activate(GameObject sourceObject = null)
    {
        if(isWalkable){
            isWalkable = false;
            UpdateSprite();
            return true;
        }
        return false;
    }

    private void UpdateSprite(){
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = activatedSprite;
    }
}
