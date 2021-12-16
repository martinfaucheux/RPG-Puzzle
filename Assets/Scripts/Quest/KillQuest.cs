using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KillQuest", menuName = "Custom Objects / Quest / KillQuest")]
public class KillQuest : Quest
{
    public int remainingEnemyCount = 0;

    public override bool CheckCompletion()
    {
        return GetEnemyRemainingCount() == remainingEnemyCount;
    }

    private int GetEnemyRemainingCount()
    {
        return GameObject.FindObjectsOfType<Health>().Length - 1;
    }
}