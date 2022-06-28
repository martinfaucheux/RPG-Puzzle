using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// <summary>
// Helper class to keep TurnActor in a listed sorted by their priority
// </summary>
public class TurnActorList : List<TurnActor>
{
    public void AddActor(TurnActor actor)
    {
        int index = 0;
        foreach (TurnActor _actor in this)
        {
            if (actor.GetPriority() < _actor.GetPriority())
                break;

            index++;
        }
        this.Insert(index, actor);
    }
}