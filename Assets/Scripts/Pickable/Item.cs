using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public Sprite sprite;

    protected virtual bool _onlyForPlayer { get; } = false;

    public void OnPickUp(GameObject pickerGameObject, int itemId)
    {
        PickUp(pickerGameObject, itemId);
        GameEvents.instance.PickItemTrigger(this);
    }

    public abstract void PickUp(GameObject pickerGameObject, int itemId);

    public virtual bool CanPickUp(GameObject pickerGameObject)
    {
        return !_onlyForPlayer | (pickerGameObject.tag == "Player");
    }
}
