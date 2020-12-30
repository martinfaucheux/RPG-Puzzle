using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Item
{
    abstract public void Consume(GameObject targetGameObject);

    public override void OnPickUp(GameObject pickerGameObject)
    {
        Consume(pickerGameObject);
    }
}
