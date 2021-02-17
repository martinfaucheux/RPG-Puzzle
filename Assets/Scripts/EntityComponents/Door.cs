using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActivableObject
{
    // return true if source object can move after activation
    public override bool Activate(GameObject sourceObject = null)
    {
        Inventory inventoryComponent = sourceObject.GetComponent<Inventory>();
        if (inventoryComponent != null)
        {
            bool hasKey = inventoryComponent.UseKey();
            if (hasKey)
            {
                Debug.Log("Player unlocked a door with a key");
                Destroy(this.gameObject, 0.05f);
                return true;
            }
        }
        return false;
    }
}
