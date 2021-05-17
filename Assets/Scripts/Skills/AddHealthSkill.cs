
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Custom Objects / Skills / AddHealthSkill")]
public class AddHealthSkill: Skill 
{
    public override void Enable(GameObject target){
        base.Enable(target);
        Health healthComp = GetComponentOrRaise<Health>(target);

        if (healthComp != null)
            healthComp.MaxHealthPoints += 1;
    }

    public override void Disable(GameObject target){
        base.Enable(target);
        Health healthComp = GetComponentOrRaise<Health>(target);

        if (healthComp != null)
            healthComp.MaxHealthPoints -= 1;
    }
}