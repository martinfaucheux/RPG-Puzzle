using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInitializer : MonoBehaviour
{
    [SerializeField] Skill[] skills;

    private SkillSlotUI[] skillSlots;
    private SkillSelectorUI skillSelector;

    void Start()
    {
        skillSlots = SkillMenu.instance.GetComponentsInChildren<SkillSlotUI>(true);
        skillSelector = SkillMenu.instance.GetComponentInChildren<SkillSelectorUI>(true);
        PopulateSkills();
    }

    private void PopulateSkills(){
        for(int i = 0; i < skills.Length; i++){
            SkillSlotUI skillSlot = skillSlots[i];
            skillSlot.skill = skills[i];
            skillSlot.SetSprite();
            skillSlot.skillSelectorUI = skillSelector;
        }
    }
}
