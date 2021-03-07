using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
        _healthComponent = GetComponent<Health>();

        targetExpPoints = GetTargetExpForLevel(1);
        UIManager.instance.UpdateUI();

        // TODO: this mechanism is no longer user
        // attach the appropirate function to each reward button
        // SetRewardButtonListeners();
    }

    public void GainExp(int expAmount)
    {
        Debug.Log(gameObject.ToString() + " gains " + expAmount.ToString() + " exp points");

        currentExpPoints += expAmount;

        if (currentExpPoints >= targetExpPoints)
        {
            LevelUp();
        }
        GameEvents.instance.PlayerExperienceChangeTrigger();
        UIManager.instance.UpdateUI();
    }

    private void LevelUp()
    {
        Debug.Log(gameObject.ToString() + " level up to " + (currentLevel + 1).ToString());

        currentExpPoints -= GetTargetExpForLevel(currentLevel);
        currentLevel++;

        targetExpPoints = GetTargetExpForLevel(currentLevel);

        if (_healthComponent)
        {
            // _healthComponent.HealFullHealth();
        }

        // trigger level up event
        GameEvents.instance.LevelUpEnterTrigger();
    }

    // TODO: this mechanism is no longer used
    private void PickLevelUpReward(LevelUpReward reward)
    {
        Debug.Log("pick reward");
        switch (reward)
        {
            case LevelUpReward.ADD_HEALTH:
                _healthComponent.AddMaxHealth();
                break;
            case LevelUpReward.ADD_ATTACK:
                Attack attackComponent = GetComponent<Attack>();
                attackComponent.AddAttackPoint();
                break;
            case LevelUpReward.HEAL:
                _healthComponent.HealFullHealth();
                break;
        }
        // GameManager.instance.ExitLevelUpScreen();
    }

    public void PickAddHealthReward() => PickLevelUpReward(LevelUpReward.ADD_HEALTH);
    public void PickAddAttackthReward() => PickLevelUpReward(LevelUpReward.ADD_ATTACK);
    public void PickHealReward() => PickLevelUpReward(LevelUpReward.HEAL);

    public static int GetTargetExpForLevel(int level)
    {
        // return level + 2;
        return 3;
    }

    private void SetRewardButtonListeners()
    {
        GameObject[] rewardButtonObjects = GameObject.FindGameObjectsWithTag("Reward Button");
        if (rewardButtonObjects.Length == 0)
        {
            Debug.LogWarning("Experience Component: no reward buttons found");
        }
        foreach(GameObject rewardButtonObject in rewardButtonObjects)
        {
            LevelUpReward rewardType = LevelUpReward.ADD_HEALTH;
            switch (rewardButtonObject.name)
            {
                case "AddHealth Button":
                    rewardType = LevelUpReward.ADD_HEALTH;
                    break;
                case "AddAttack Button":
                    rewardType = LevelUpReward.ADD_ATTACK;
                    break;
                case "Heal Button":
                    rewardType = LevelUpReward.HEAL;
                    break;
                default:
                    Debug.LogError("Unknown reward button name: " + rewardButtonObject.name);
                    break;
            }

            Button rewardButtonComponent = rewardButtonObject.GetComponent<Button>();
            rewardButtonComponent.onClick.AddListener(() => PickLevelUpReward(rewardType));
        }
    }
	
}
