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
        SetButtonInteractable(skill);
    }

    public void ClearSkill(){
        skillNameTextComponent.text = "";
        skillDescriptionTextComponent.text = "";
        SetButtonInteractable();
    }

    public void UnlockSkill(){
        bool result = SkillManager.instance.Unlock(_selectedSkill);
        if (!result){
            SkillCounterWizzer.instance?.Wizz();
        }
        SetButtonInteractable(_selectedSkill);
    }

    private void SetButtonInteractable() => SetButtonInteractable(null);

    private void SetButtonInteractable(Skill skill){
        bool interactable = false;

        if(skill != null){
            interactable = (
                !SkillManager.instance.IsUnlocked(skill)
                && SkillManager.instance.skillPoints > 0
            );
        }
        unlockSkillButton.interactable = interactable;
    }

}
