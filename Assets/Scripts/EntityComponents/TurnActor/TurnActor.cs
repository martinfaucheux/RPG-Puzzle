using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnActor : MonoBehaviour
{
    [Tooltip("Defines the order of execution of turn. 0 if the highest priority.")]
    [SerializeField] int _priority;
    [SerializeField] protected MovingObject movingObject;

    public int GetPriority() => _priority;
    protected virtual void Start()
    {
        if (movingObject == null)
            Debug.LogError("TurnActor: movingObject is null");

        TurnManager.instance.Add(this);
    }
    protected virtual void OnDestroy()
    {
        TurnManager.instance.Remove(this);
    }
    public abstract IEnumerator DoTurn();
}
