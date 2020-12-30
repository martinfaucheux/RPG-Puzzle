using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storable : Item
{
    abstract public void Store(GameObject targetGameObject);

    public override void OnPickUp(GameObject pickerGameObject)
    {
        Store(pickerGameObject);
    }
}
