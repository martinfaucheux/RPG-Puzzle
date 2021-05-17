using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSelectorUI : MonoBehaviour
{
    public TextMeshProUGUI skillNameTextComponent;
    public TextMeshProUGUI skillDescriptionTextComponent;

    private Skill _selectedSkill;

    [SerializeField] Button unlockSkillButton;

    void Start(){
        ClearSkill();
    }

    public void SelectSkill(Skill skill){
        _selectedSkill = skill;
        skillNameTextComponent.text = skill.skillName;
        skillDescriptionTextComponent.text = skill.skillDescription;

        unlockSkillButton.interactable = !skill.isEnabled;
    }

    public void ClearSkill(){
        _selectedSkill = null;
        skillNameTextComponent.text = "";
        skillDescriptionTextComponent.text = "";

        unlockSkillButton.interactable = false;
    }

    public void UnlockSkill(){
        bool result = SkillManager.instance.Enable(_selectedSkill);

        Debug.Log(result);

        unlockSkillButton.interactable = !result;
        if (!result){
            SkillCounterWizzer.instance.Wizz();
        }
    }
}
