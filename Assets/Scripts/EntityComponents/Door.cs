using System.Collections;
using UnityEngine;

public class Door : ActivableObject
{
    public override bool CheckAllowInteraction(GameObject sourceObject)
    {
        Inventory inventory = sourceObject.GetComponent<Inventory>();
        return inventory != null && inventory.keyCount > 0;
    }

    public override bool CheckAllowMovement(GameObject sourceObject)
    {
        return true;
    }

    // return true if source object can move after activation
    public override IEnumerator Activate(GameObject sourceObject)
    {
        Inventory inventory = sourceObject.GetComponent<Inventory>();
        if (inventory != null && inventory.keyCount > 0)
        {
            inventory.UseKey();

            // TODO: move this at the right place
            GameEvents.instance.UserItemTrigger((Key)ScriptableObject.CreateInstance(typeof(Key)));
            Destroy(this.gameObject, 0.05f);

        }
        yield return null;
    }


}
