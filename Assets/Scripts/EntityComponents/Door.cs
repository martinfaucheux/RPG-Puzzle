using System.Collections;
using UnityEngine;

public class Door : ActivableObject
{
    // return true if source object can move after activation
    public override IEnumerator Activate(GameObject sourceObject)
    {
        allowMovement = false;
        Inventory inventoryComponent = sourceObject.GetComponent<Inventory>();
        if (inventoryComponent != null)
        {
            bool hasKey = inventoryComponent.UseKey();
            if (hasKey)
            {
                Debug.Log("Player unlocked a door with a key");

                // TODO: move this at the right place
                GameEvents.instance.UserItemTrigger((Key)ScriptableObject.CreateInstance(typeof(Key)));

                allowMovement = true;
                Destroy(this.gameObject, 0.05f);
            }
        }
        yield return allowMovement;
    }
}
