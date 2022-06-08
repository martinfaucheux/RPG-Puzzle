using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class SkillSelectorUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI skillNameTextComponent;
    [SerializeField] TextMeshProUGUI skillDescriptionTextComponent;
    [SerializeField] string defaultDescription;
    [SerializeField] Button unlockSkillButton;
    [SerializeField] TextMeshProUGUI buttonText;
    List<SkillSlotUI> _skillSlots = new List<SkillSlotUI>();

    private SkillSlotUI _selectedSkillSlot;

    private Skill _selectedSkill
    {
        get
        {
            if (_selectedSkillSlot != null)
                return _selectedSkillSlot.skill;
            return null;
        }
    }


    void Start()
    {
        UpdateUI();
        GameEvents.instance.onCloseSkillMenu += UnselectSkill;
    }

    void OnDestrot()
    {
        GameEvents.instance.onCloseSkillMenu -= UnselectSkill;
    }

    public void Register(SkillSlotUI skillSlot)
    {
        _skillSlots.Add(skillSlot);
    }

    public void Toggle(SkillSlotUI skillSlot)
    {
        // called when a skill button is clicked
        if (skillSlot == _selectedSkillSlot)
        {
            // TODO: update buttons color
            _selectedSkillSlot = null;
            //  EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            // TODO: update buttons color
            _selectedSkillSlot = skillSlot;
            _selectedSkillSlot.SetButtonSelect();
        }
        UpdateUI();
    }

    private void UnselectSkill()
    {
        _selectedSkillSlot = null;
        UpdateUI();
    }

    public void UnlockSelectedSkill()
    {
        bool result = SkillManager.instance.Unlock(_selectedSkill);
        if (!result)
        {
            SkillCounterWizzer.instance?.Wizz();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        SetSkillText();
        SetUnlockButtonInteractable();
        SetButtonText();
    }

    private void SetUnlockButtonInteractable()
    {
        // TODO: doesn't work all the time
        bool interactable = false;

        if (_selectedSkill != null)
        {
            interactable = (
                !SkillManager.instance.IsUnlocked(_selectedSkill)
                && SkillManager.instance.skillPoints > 0
            );
        }
        unlockSkillButton.interactable = interactable;
    }

    private void SetButtonText()
    {
        string text = "Unlock";
        if (_selectedSkill != null && SkillManager.instance.IsUnlocked(_selectedSkill))
        {
            text = "Unlocked!";
        }
        buttonText.text = text;
    }

    private void SetSkillText()
    {
        string skillName = "";
        string skillDescription = "";
        if (_selectedSkill != null)
        {
            skillName = _selectedSkill.skillName;
            skillDescription = _selectedSkill.skillDescription;

        }
        skillNameTextComponent.text = skillName;
        skillDescriptionTextComponent.text = skillDescription;

    }
}
