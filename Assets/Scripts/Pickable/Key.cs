using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Custom Objects / Key")]
public class Key : Storable
{
    protected override bool _onlyForPlayer { get; } = true;

    public override void Store(GameObject targetGameObject)
    {
        Inventory inventoryComponent = targetGameObject.GetComponent<Inventory>();
        if (inventoryComponent != null)
        {
            inventoryComponent.AddKey();
        }
        else
        {
            Debug.LogError(targetGameObject.ToString() + ": no Inventory component found");
        }
    }
}
