using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public Sprite sprite;
    [SerializeField] string pickUpSoundName;
    protected virtual bool _onlyForPlayer { get; } = false;

    protected PickableObject parentComponent;


    public void OnPickUp(GameObject pickerGameObject)
    {
        PickUp(pickerGameObject);

        if (pickUpSoundName.Length > 0)
        {
            AudioManager.instance.Play(pickUpSoundName);
        }

        GameEvents.instance.PickItemTrigger(this);
    }

    public abstract void PickUp(GameObject pickerGameObject);

    public virtual bool CanPickUp(GameObject pickerGameObject)
    {
        return !_onlyForPlayer | (pickerGameObject.tag == "Player");
    }

    public virtual void Initialize(PickableObject parentComponent)
    {
        this.parentComponent = parentComponent;
    }
}
