using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    public Skill skill { get; private set; }
    private SkillSelectorUI _skillSelectorUI;
    [SerializeField] Image _skillIconImage;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Button _button;
    [Tooltip("Used to select the first level on scene load.")]
    [SerializeField] SelectOnEnableUI _selectOnEnableUI;


    void Start()
    {
        _button = GetComponent<Button>();
        GameEvents.instance.onSkillEnabled += OnUnlockSkill;

        // if first child, select the object on enable
        if (transform.GetSiblingIndex() == 0)
        {
            _selectOnEnableUI.enabled = true;
        }
    }

    public void Initialize(Skill skill, SkillSelectorUI skillSelectorUI)
    {
        this.skill = skill;
        _skillSelectorUI = skillSelectorUI;
        _skillIconImage.sprite = skill.sprite;
        SkillManager.instance.RegisterSkill(skill);
        skillSelectorUI.Register(this);
    }

    public void OnSkillSelect()
    {
        // TODO: this gets null reference error
        _skillSelectorUI.Toggle(this);
    }

    public void UnlockSelectedSkill()
    {
        _skillSelectorUI.UnlockSelectedSkill();
    }

    private void OnUnlockSkill(Skill skill)
    {
        if (skill == this.skill)
        {
            _canvasGroup.alpha = 0.5f;
        }
    }

    public void SetButtonSelect() => _button.Select();

    void OnDestroy()
    {
        GameEvents.instance.onSkillEnabled -= OnUnlockSkill;
    }
}
