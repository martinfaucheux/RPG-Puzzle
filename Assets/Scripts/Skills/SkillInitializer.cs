using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInitializer : MonoBehaviour
{
    [SerializeField] Skill[] skills;

    SkillSlotUI[] skillSlots;

    void Start()
    {
        skillSlots = SkillMenu.instance.GetComponentsInChildren<SkillSlotUI>(true);
        PopulateSkills();
    }

    private void PopulateSkills(){
        for(int i = 0; i < skills.Length; i++){
            skillSlots[i].skill = skills[i];
            skillSlots[i].SetSprite();
        }
    }
}
