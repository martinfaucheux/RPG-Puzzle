using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInitializer : MonoBehaviour
{
    [SerializeField] Skill[] skills;

    private SkillSlotUI[] _skillSlots;
    private SkillSelectorUI _skillSelector;

    void Start()
    {
        _skillSlots = SkillMenu.instance.GetComponentsInChildren<SkillSlotUI>(true);
        _skillSelector = SkillMenu.instance.GetComponentInChildren<SkillSelectorUI>(true);
        PopulateSkills();
    }

    private void PopulateSkills()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            _skillSlots[i].Initialize(skills[i], _skillSelector);
        }
    }
}
