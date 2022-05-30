using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnActor : MonoBehaviour
{
    protected virtual void Start()
    {
        TurnManager.instance.Add(this);
    }

    protected virtual void OnDestroy()
    {
        TurnManager.instance.Remove(this);
    }
    public abstract IEnumerator DoTurn();
}
