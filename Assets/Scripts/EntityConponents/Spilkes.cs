using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spilkes : ActivableObject
{
    public int damage = 1;

    // return true if source object can move after activation
    public override bool Activate(GameObject sourceObject = null)
    {
        Health healthComponent = sourceObject.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);
        }
        return true;
    }
}