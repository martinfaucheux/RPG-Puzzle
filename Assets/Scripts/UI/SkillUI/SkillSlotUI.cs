using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    public Skill skill { get; private set; }
    private SkillSelectorUI skillSelectorUI;
    [SerializeField] Image _skillIconImage;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Button _button;


    void Start()
    {
        _button = GetComponent<Button>();
        GameEvents.instance.onSkillEnabled += OnUnlockSkill;
    }

    public void Initialize(Skill skill, SkillSelectorUI skillSelectorUI)
    {
        this.skill = skill;
        this.skillSelectorUI = skillSelectorUI;
        _skillIconImage.sprite = skill.sprite;
        SkillManager.instance.RegisterSkill(skill);
        skillSelectorUI.Register(this);
    }

    public void OnClick()
    {
        skillSelectorUI.Toggle(this);
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
