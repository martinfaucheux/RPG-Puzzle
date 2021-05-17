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
        SetButtonInteractable();
        skillNameTextComponent.text = "";
        skillDescriptionTextComponent.text = "";

        unlockSkillButton.interactable = false;
    }

    public void UnlockSkill(){
        bool result = SkillManager.instance.Enable(_selectedSkill);
        SetButtonInteractable(_selectedSkill);
        
        if (!result){
            SkillCounterWizzer.instance?.Wizz();
        }
    }

    private void SetButtonInteractable() => SetButtonInteractable(null);

    private void SetButtonInteractable(Skill skill){
        bool interactable = false;

        if(skill != null){
            interactable = !SkillManager.instance.IsUnlocked(skill);
        }

        unlockSkillButton.interactable = interactable;
    }

}
