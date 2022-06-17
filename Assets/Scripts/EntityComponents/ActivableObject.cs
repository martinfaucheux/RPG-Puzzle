using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivableObject : MonoBehaviour
{
    protected MatrixCollider _matrixCollider;
    protected virtual void Start()
    {
        _matrixCollider = GetComponent<MatrixCollider>();
    }

    public virtual bool CheckAllowInteraction(GameObject sourceObject)
    {
        return !_matrixCollider.isBlocking;
    }

    public abstract bool CheckAllowMovement(GameObject sourceObject);

    // return true if source object can move after activation
    public virtual IEnumerator Activate(GameObject sourceObject) { yield return null; }

    // function that will be ran once the player exit the case
    public virtual void OnLeave() { }
}
