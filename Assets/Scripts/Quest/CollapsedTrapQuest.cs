using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollapsedTrapQuest", menuName = "Custom Objects / Quest / Collapsed Trap Quest")]
public class CollapsedTrapQuest : Quest
{
    public int remainingTrapCount = 0;
    public override bool CheckCompletion()
    {
        int trapCount = 0;
        Crack[] trapComponents = GameObject.FindObjectsOfType<Crack>();
        foreach (Crack trapComponent in trapComponents)
        {
            if (trapComponent.isWalkable)
            {
                trapCount++;
            }
        }
        return remainingTrapCount == trapCount;
    }
}
