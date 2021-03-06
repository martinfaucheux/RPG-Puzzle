
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Custom Objects / Skills / RestoreHealth")]
public class RestoreHealthSkill: Skill 
{
    public override void Enable(GameObject target){
        base.Enable(target);
        target.GetComponent<Health>().HealFullHealth();
    }

    public override void Disable(GameObject target){
        base.Enable(target);
        // TODO: keep in memry the amount of health restored
    }
}