using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Item
{
    protected abstract void Consume(GameObject targetGameObject, int itemId);

    public override void PickUp(GameObject pickerGameObject, int itemId)
    {
        Consume(pickerGameObject, itemId);
        GameEvents.instance.UserItemTrigger(this);
    }
}
