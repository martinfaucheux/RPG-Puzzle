using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KillQuest", menuName = "Custom Objects / Quest / KillQuest")]
public class KillQuest : Quest
{
    public int remainingEnemyCount = 0;
    public string specificEntityName;

    public override bool CheckCompletion()
    {
        return GetRemainingEnemyCount() == remainingEnemyCount;
    }

    private int GetRemainingEnemyCount()
    {
        int enemyCount = 0;

        Fighter[] enemyComponents = GameObject.FindObjectsOfType<Fighter>();

        if (specificEntityName.Length == 0)
        {
            enemyCount = enemyComponents.Length;
        }
        else
        {
            foreach (Fighter enemyComponent in enemyComponents)
            {
                if (enemyComponent.GetComponent<EntityInspector>().GetName() == specificEntityName)
                {
                    enemyCount++;
                }
            }
        }
        return enemyCount;
    }
}