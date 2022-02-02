using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemQuest", menuName = "Custom Objects / Quest / ItemQuest")]
public class ItemQuest : Quest
{
    public int remainingItemCount = 0;
    public Item item;

    public override bool CheckCompletion()
    {
        return GetRemainingItemCount() == remainingItemCount;
    }

    private int GetRemainingItemCount()
    {
        int itemCount = 0;

        PickableObject[] pickableObjects = GameObject.FindObjectsOfType<PickableObject>();

        if (item == null)
        {
            itemCount = pickableObjects.Length;
        }
        else
        {
            foreach (PickableObject pickableObject in pickableObjects)
            {
                if (pickableObject.item == item)
                {
                    itemCount++;
                }
            }
        }
        return itemCount;
    }
}
