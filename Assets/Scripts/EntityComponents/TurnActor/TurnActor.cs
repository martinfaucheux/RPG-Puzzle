using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnActor : MonoBehaviour
{
    [Tooltip("Defines the order of execution of turn. 0 if the highest priority.")]
    [SerializeField] int _priority;
    [field: SerializeField]
    public MovingObject movingObject { get; private set; }

    public int GetPriority()
    {
        return 100 * _priority + transform.GetSiblingIndex();
    }

    protected virtual void Start()
    {
        if (movingObject == null)
            movingObject = GetComponent<MovingObject>();
        if (movingObject == null)
            Debug.LogError("TurnActor: movingObject is null");
        else
            Debug.LogWarning("TurnActor: movingObject not set in inspector");

        TurnManager.instance.Add(this);
    }
    protected virtual void OnDestroy()
    {
        // TurnManager.instance.Remove(this);
    }
    public abstract IEnumerator DoTurn();
}
