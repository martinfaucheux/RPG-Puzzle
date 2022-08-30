using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{

    public enum LevelUpReward
    {
        ADD_HEALTH,
        ADD_ATTACK,
        HEAL,
    }

    public int currentLevel = 1;
    public int currentExpPoints = 0;
    public int targetExpPoints;

    private Health _healthComponent;

    void Start()
    {
        _healthComponent = GetComponent<Health>();
        targetExpPoints = GetTargetExpForLevel(1);
    }

    public void GainExp(int expAmount)
    {
        currentExpPoints += expAmount;

        if (currentExpPoints >= targetExpPoints)
        {
            LevelUp();
        }
        GameEvents.instance.PlayerExperienceChangeTrigger();
    }

    private void LevelUp()
    {
        currentExpPoints -= GetTargetExpForLevel(currentLevel);
        currentLevel++;
        targetExpPoints = GetTargetExpForLevel(currentLevel);
        GameEvents.instance.LevelUpTrigger();
    }

    public static int GetTargetExpForLevel(int level)
    {
        // return level + 2;
        return 3;
    }


}
