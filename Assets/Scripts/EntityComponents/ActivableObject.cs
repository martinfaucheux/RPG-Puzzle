using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableObject : MonoBehaviour
{
    public bool allowMovement { get; protected set; } = true;

    // return true if source object can move after activation
    public virtual IEnumerator Activate(GameObject sourceObject)
    {
        Debug.Log(gameObject.ToString() + ": Activated");
        allowMovement = true;
        yield return allowMovement;
    }

    // function that will be ran once the player exit the case
    public virtual void OnLeave()
    {
        return;
    }
}
