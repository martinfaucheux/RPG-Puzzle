using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : UIBarElement
{
    // sprite images
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    public override void SetValue(bool value)
    {
        if (value)
        {
            imageComponent.sprite = fullHeartSprite;
        }
        else
        {
            imageComponent.sprite = emptyHeartSprite;
        }
    }

    public void SetToFull()
    {
        imageComponent.sprite = fullHeartSprite;
    }

    public void SetToEmpty()
    {
        imageComponent.sprite = emptyHeartSprite;
    }
}
