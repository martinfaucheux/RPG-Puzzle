using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivableObject : MonoBehaviour
{
    [field: Tooltip("Order to resolve interaction of ActivableObjects.")]
    [field: SerializeField]
    public int interactionPriority { get; protected set; } = 5;
    protected MatrixCollider _matrixCollider;
    protected virtual void Start()
    {
        _matrixCollider = GetComponent<MatrixCollider>();
    }

    public virtual bool CheckAllowInteraction(GameObject sourceObject)
    {
        return !_matrixCollider.isBlocking;
    }

    public virtual IEnumerator OnInteract(GameObject sourceObject) { yield return null; }

    public virtual bool CheckAllowMovement(GameObject sourceObject) => true;

    // return true if source object can move after activation
    public virtual IEnumerator OnEnter(GameObject sourceObject) { yield return null; }

    // function that will be ran once the player leave the cell
    public virtual void OnLeave() { }
}
