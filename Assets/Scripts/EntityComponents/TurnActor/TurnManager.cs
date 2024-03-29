using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : SingletoneBase<TurnManager>
{
    public bool isReady { get; private set; } = true;
    public PlayerTurnActor playerTurnActor { get; private set; }
    private TurnActorList _actors = new TurnActorList();
    private bool _stopRequest = false;

    public void Add(TurnActor actor)
    {
        _actors.AddActor(actor);
        if (actor.gameObject.tag == "Player")
            playerTurnActor = actor as PlayerTurnActor;
    }

    public void Remove(TurnActor actor) => _actors.Remove(actor);

    public void DoTurn() => StartCoroutine(DoTurnCoroutine());

    public void StopTurn() => _stopRequest = true;

    private IEnumerator DoTurnCoroutine()
    {
        isReady = false;
        GameEvents.instance.TurnStartTrigger();
        foreach (TurnActor actor in _actors)
        {
            // actor might be set to null if it died
            if (actor != null && !_stopRequest)
            {
                yield return StartCoroutine(actor.DoTurn());
            }
        }
        GameEvents.instance.TurnEndTrigger();
        isReady = true;
    }
}
