using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : SingletoneBase<TurnManager>
{
    public bool isReady { get; private set; } = true;
    private bool _stopRequest = false;

    private TurnActorList _actors = new TurnActorList();

    public void Add(TurnActor actor) => _actors.AddActor(actor);

    public void Remove(TurnActor actor) => _actors.Remove(actor);

    public void DoTurn() => StartCoroutine(DoTurnCoroutine());

    public void StopTurn() => _stopRequest = true;

    private IEnumerator DoTurnCoroutine()
    {
        isReady = false;
        GameEvents.instance.TurnStartTrigger();
        foreach (TurnActor actor in _actors)
        {
            if (!_stopRequest)
            {
                yield return StartCoroutine(actor.DoTurn());
            }
        }
        GameEvents.instance.TurnEndTrigger();
        isReady = true;
    }
}
