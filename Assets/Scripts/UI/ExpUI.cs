using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExpUI : UIBarElement
{
    public Sprite expEmptySprite;
    public Sprite expFullSprite;

    public override void SetValue(bool expValue)
    {
        Sprite expSprite = expEmptySprite;
        if (expValue)
        {
            expSprite = expFullSprite;
        }

        imageComponent.sprite = expSprite;
        imageComponent.enabled = true;
    }
}
