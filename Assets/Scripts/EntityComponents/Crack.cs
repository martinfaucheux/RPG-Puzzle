using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : ActivableObject
{
    public Sprite activatedSprite;

    // this needs to be available for quests
    public bool isWalkable
    {
        get; private set;
    } = true;

    public override bool CheckAllowInteraction(GameObject sourceObject)
    {
        return isWalkable;
    }

    public override void OnLeave()
    {
        if (isWalkable)
        {
            isWalkable = false;
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = activatedSprite;
    }
}
