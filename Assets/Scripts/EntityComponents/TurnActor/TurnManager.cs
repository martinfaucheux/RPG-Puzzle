using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : SingletoneBase<TurnManager>
{
    public bool isReady { get; private set; } = true;
    // [SerializeField] float defaultTurnDuration = -1f;

    private static List<TurnActor> _actors = new List<TurnActor>();

    public void Add(TurnActor actor)
    {
        _actors.Add(actor);
    }

    public void Remove(TurnActor actor)
    {
        _actors.Remove(actor);
    }

    public void DoTurn()
    {
        StartCoroutine(DoTurnCoroutine());
    }

    private IEnumerator DoTurnCoroutine()
    {
        isReady = false;
        foreach (TurnActor actor in _actors)
        {
            yield return StartCoroutine(actor.DoTurn());
        }
        isReady = true;
    }


}
