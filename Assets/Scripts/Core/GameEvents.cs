using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance = null;

    public event Action onEnterLevelUp;
    public event Action<int> onHealthChange;
    public event Action onPlayerGetDamage;
    public event Action onPlayerExperienceChange;
    public event Action onEndOfLevel;
    public event Action onGameOver;

    public event Action onSkillEnabled;

    # region Singleton

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a CollisionMatrix.
            Destroy(gameObject);
    }

    # endregion

    public void LevelUpEnterTrigger()
    {
        if (onEnterLevelUp != null)
        {
            onEnterLevelUp();
        }
    }

    public void HealthChangeTrigger(int healthID)
    {
        if (onHealthChange != null)
        {
            onHealthChange(healthID);
        }
    }

    public void PlayerGetDamage()
    {
        if (onPlayerGetDamage != null)
        {
            onPlayerGetDamage();
        }
    }

    public void PlayerExperienceChangeTrigger()
    {
        if (onPlayerExperienceChange != null)
        {
            onPlayerExperienceChange();
        }
    }

    public void EndOfLevelTrigger()
    {
        if (onEndOfLevel != null)
        {
            onEndOfLevel();
        }
    }

    public void GameOverTrigger()
    {
        if (onGameOver != null)
        {
            onGameOver();
        }
    }

    public void SkillEnabledTrigger()
    {
        if (onSkillEnabled != null)
        {
            onSkillEnabled();
        }
    }

}
