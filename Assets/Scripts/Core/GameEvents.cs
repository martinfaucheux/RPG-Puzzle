using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance = null;

    public event Action onEnterLevelUp;
    public event Action<int, int, int> onHealthChange;
    public event Action<int> onAttackChange;
    public event Action onPlayerGetDamage;
    public event Action onPlayerExperienceChange;
    public event Action onEndOfLevel;
    public event Action onGameOver;

    public event Action<Skill> onSkillEnabled;
    public event Action onOpenSkillMenu;
    public event Action onCloseSkillMenu;

    public event Action<Item> onPickItem;
    public event Action<Item> onUseItem;
    public event Action<Health> onUnitDies;
    public event Action<Vector2Int> onPlayerMove;

    public event Action<GameState> onEnterState;
    public event Action<GameState> onExitState;

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

    public void HealthChangeTrigger(int healthID, int initValue, int finalValue)
    {
        if (onHealthChange != null)
        {
            onHealthChange(healthID, initValue, finalValue);
        }
    }

    public void AttackChangeTrigger(int attackID)
    {
        if (onAttackChange != null)
        {
            onAttackChange(attackID);
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

    public void SkillEnabledTrigger(Skill skill)
    {
        if (onSkillEnabled != null)
        {
            onSkillEnabled(skill);
        }
    }

    public void PickItemTrigger(Item item)
    {
        if (onPickItem != null)
        {
            onPickItem(item);
        }
    }

    public void UserItemTrigger(Item item)
    {
        if (onUseItem != null)
        {
            onUseItem(item);
        }
    }

    public void OpenSkillMenuTrigger()
    {
        if (onOpenSkillMenu != null)
        {
            onOpenSkillMenu();
        }
    }

    public void CloseSkillMenuTrigger()
    {
        if (onCloseSkillMenu != null)
        {
            onCloseSkillMenu();
        }
    }

    public void UnitDiesTrigger(Health healthComponent)
    {
        if (onUnitDies != null)
        {
            onUnitDies(healthComponent);
        }
    }

    public void PlayerMoveTrigger(Vector2Int position)
    {
        if (onPlayerMove != null)
        {
            onPlayerMove(position);
        }
    }

    public void EnterStateTrigger(GameState state)
    {
        if (onEnterState != null)
        {
            onEnterState(state);
        }
    }

    public void ExitStateTrigger(GameState state)
    {
        if (onExitState != null)
        {
            onExitState(state);
        }
    }

}
