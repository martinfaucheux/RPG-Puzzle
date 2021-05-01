using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    // public int maxHealthPoints;
    public int CurrentHealthPoints
    {
        get { return _currentHealth; }
        set {
            _currentHealth = value;
            GameEvents.instance.HealthChangeTrigger(GetInstanceID());
        }
    }

    public int MaxHealthPoints
    {
        get { return _maxHealthPoints; }
        set
        {
            _maxHealthPoints = value;
            GameEvents.instance.HealthChangeTrigger(GetInstanceID());
        }
    }

    public int expRewardPoints = 1;


    public bool isDead = false;
    public HealthDisplay healthDisplay = null;

    [SerializeField]
    private int _currentHealth;
    [SerializeField]
    private int _maxHealthPoints;
    private SpriteFlasher _spriteFlasher;
    

    // Use this for initialization
    void Start () {

        CurrentHealthPoints = MaxHealthPoints;
        _spriteFlasher = GetComponent<SpriteFlasher>();
        UpdateUI();
	}

    public float GetHealthPercentage()
    {
        return (float) CurrentHealthPoints / (float) MaxHealthPoints;
    }

    public void TakeDamage(int damagePoints)
    {
        Debug.Log(gameObject.ToString() + " takes " + damagePoints.ToString() + " damage points");
        int newValue = CurrentHealthPoints - damagePoints;
        if (newValue < 0)
            newValue = 0;

        CurrentHealthPoints = newValue;

        UpdateUI();

        if (tag == "Player"){
            GameEvents.instance.PlayerGetDamage();
        }

        if (damagePoints > 0 && _spriteFlasher != null){
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
        UpdateUI();
    }

    public void HealFullHealth()
    {
        Heal(MaxHealthPoints);
    }

    public void AddMaxHealth(int healthPoints = 1)
    {
        MaxHealthPoints += healthPoints;
        CurrentHealthPoints += healthPoints;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (healthDisplay != null)
        {
            healthDisplay.UpdateUI();
        }
    }


    public void Die()
    {
        isDead = true;
        Debug.Log(gameObject.ToString() + " is dead");

        // TODO: play animation here

        if (tag != "Player")
        {
            Destroy(gameObject);
        }
        else
        {
            GameManager.instance.GameOver();
        }
        
    }
}
