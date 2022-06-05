using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spilkes : ActivableObject
{
    public int damage = 1;
    [SerializeField] string soundName;

    // return true if source object can move after activation
    public override IEnumerator Activate(GameObject sourceObject = null)
    {
        Health healthComponent = sourceObject.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);
            AudioManager.instance.Play(soundName);
        }
        allowMovement = true;
        yield return allowMovement;
    }
}