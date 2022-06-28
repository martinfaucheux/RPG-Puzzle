using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : ActivableObject
{

    public Item item;
    public int itemId;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (item == null)
            Debug.LogError("No item found");
        else
            item.Initialize(this);
    }

    public override bool CheckAllowMovement(GameObject sourceObject)
    {
        return true;
    }

    public void OnValidate()
    {
        if (item != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = item.sprite;
            }
        }
    }

    // return true if source object can move after activation
    public override IEnumerator OnEnter(GameObject sourceObject)
    {
        if (item.CanPickUp(sourceObject))
        {
            item.OnPickUp(sourceObject);
            Destroy(gameObject, 0.05f); // delete the object shortly after
        }
        yield return null;
    }
}
