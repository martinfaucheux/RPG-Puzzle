﻿using System.Collections;
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
    private SpriteHolder _spriteHolder;
    private Animator _animator { get { return _spriteHolder!.activeAnimator; } }
    private static string DIE_ANIMATION = "die";

    // Use this for initialization
    void Start()
    {
        CurrentHealthPoints = MaxHealthPoints;
        _spriteFlasher = GetComponent<SpriteFlasher>();
        _spriteHolder = GetComponent<SpriteHolder>();
    }

    public float GetHealthPercentage()
    {
        return (float)CurrentHealthPoints / (float)MaxHealthPoints;
    }

    public void TakeDamage(int damagePoints)
    {
        int newValue = CurrentHealthPoints - damagePoints;
        if (newValue < 0)
            newValue = 0;

        CurrentHealthPoints = newValue;

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
        if (_animator != null && AnimatorUtils.HasParameter(_animator, DIE_ANIMATION))
        {
            _spriteHolder.FaceDirection(Direction.DOWN);
            _animator.SetTrigger(DIE_ANIMATION);
        }

        if (tag != "Player")
        {
            // TODO: disable collider
            Destroy(gameObject);
        }
        else
        {
            GameManager.instance.GameOver();
        }

    }
}
