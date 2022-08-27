using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Custom Objects / HealthPotion")]
public class HealthPotion : Consumable
{
    public bool healFullHealth = false;
    public int healthAmount = 1;
    public bool destroyOnUse = true;

    public HealthPotion(int healthAmount)
    {
        this.healthAmount = healthAmount;
    }

    protected override void Consume(GameObject targetGameObject)
    {
        Health healthComponent = targetGameObject.GetComponent<Health>();
        if (healthComponent != null)
        {
            if (healFullHealth)
            {
                healthComponent.HealFullHealth();
            }
            else
            {
                healthComponent.Heal(healthAmount);
            }
        }
    }
}
