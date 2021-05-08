using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Item
{
    protected abstract void Consume(GameObject targetGameObject);

    public override void PickUp(GameObject pickerGameObject)
    {
        Consume(pickerGameObject);
        GameEvents.instance.UserItemTrigger(this);
    }
}
