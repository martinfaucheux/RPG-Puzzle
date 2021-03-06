
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Custom Objects / Skills / AddAttackSkill")]
public class AddAttackSkill: Skill 
{
    public override void Enable(GameObject target){
        base.Enable(target);
        target.GetComponent<Attack>().attackPoints += 1;
    }

    public override void Disable(GameObject target){
        base.Enable(target);
        target.GetComponent<Attack>().attackPoints -= 1;
    }
}