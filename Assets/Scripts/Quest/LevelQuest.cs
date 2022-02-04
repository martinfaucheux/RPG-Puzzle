using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelQuest", menuName = "Custom Objects / Quest / Level Quest")]
public class LevelQuest : Quest
{
    public int levelToReach = 2;
    public bool exact = true;
    public override bool CheckCompletion()
    {
        int playerLevel = (
            GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<Experience>()
            .currentLevel
        );
        if (exact)
        {
            return playerLevel == levelToReach;
        }
        else
        {
            return playerLevel >= levelToReach;
        }
    }
}
