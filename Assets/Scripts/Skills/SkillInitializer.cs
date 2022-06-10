using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// <summary>
// Initialize the skill set for the current level
// TODO: need to take the skill list from SkillManager
// or ScriptableObject
// </summary>
public class SkillInitializer : MonoBehaviour
{
    [SerializeField] Skill[] skills;
    [SerializeField] SkillButtonUI[] _skillSlots;



    void Start()
    {
        PopulateSkills();
    }

    private void PopulateSkills()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            _skillSlots[i].Initialize(skills[i]);
        }
    }
}
