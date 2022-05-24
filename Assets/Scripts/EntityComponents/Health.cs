using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // public int maxHealthPoints;
    public int CurrentHealthPoints
    {
        get { return _currentHealth; }
        set
        {
            int initValue = _currentHealth;
            _currentHealth = value;
            GameEvents.instance.HealthChangeTrigger(GetInstanceID(), initValue, value);
        }
    }

    public int MaxHealthPoints
    {
        get { return _maxHealthPoints; }
        set
        {
            int initValue = _currentHealth;
            _maxHealthPoints = value;
            GameEvents.instance.HealthChangeTrigger(GetInstanceID(), initValue, value);
        }
    }

    public int expRewardPoints = 1;


    public bool isDead = false;

    [SerializeField] int _currentHealth;
    [SerializeField] int _maxHealthPoints;
    private SpriteFlasher _spriteFlasher;


    // Use this for initialization
    void Start()
    {

        CurrentHealthPoints = MaxHealthPoints;
        _spriteFlasher = GetComponent<SpriteFlasher>();
    }

    public float GetHealthPercentage()
    {
        return (float)CurrentHealthPoints / (float)MaxHealthPoints;
    }

    public void TakeDamage(int damagePoints)
    {
        Debug.Log(gameObject.ToString() + " takes " + damagePoints.ToString() + " damage points");
        int newValue = CurrentHealthPoints - damagePoints;
        if (newValue < 0)
            newValue = 0;

        CurrentHealthPoints = newValue;

        if (tag == "Player")
        {
            // TODO: use HealthChange event instead
            GameEvents.instance.PlayerGetDamage();
        }

        if (damagePoints > 0 && _spriteFlasher != null)
        {
            _spriteFlasher.Flash();
        }

        if (CurrentHealthPoints <= 0)
        {
            Die();
        }
    }

    public void Heal(int healedHealthPoints)
    {
        CurrentHealthPoints = Mathf.Min(healedHealthPoints + CurrentHealthPoints, MaxHealthPoints);
    }

    public void HealFullHealth()
    {
        Heal(MaxHealthPoints);
    }

    public void AddMaxHealth(int healthPoints = 1)
    {
        MaxHealthPoints += healthPoints;
        CurrentHealthPoints += healthPoints;
    }


    public void Die()
    {
        isDead = true;
        Debug.Log(gameObject.ToString() + " is dead");

        // TODO: play animation here

        if (tag != "Player")
        {
            GameEvents.instance.UnitDiesTrigger(this);
            Destroy(gameObject);
        }
        else
        {
            GameManager.instance.GameOver();
        }

    }
}
