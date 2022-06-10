using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour
{
    [SerializeField] float _unavailableAlpha = 0.5f;
    [SerializeField] Color _unlockedColor;
    [SerializeField] Color _lockedColor;
    [SerializeField] Image _iconImage;
    [SerializeField] Image _bgImage;
    [SerializeField] Button _button;
    [SerializeField] CanvasGroup _canvasGroup;



    private Skill skill;

    void Start()
    {
        SetColor();

        GameEvents.instance.onSkillEnabled += SetColor;
        GameEvents.instance.onEnterLevelUp += SetColor;
    }

    void OnDestroy()
    {
        GameEvents.instance.onSkillEnabled -= SetColor;
        GameEvents.instance.onEnterLevelUp -= SetColor;
    }

    private bool isUnlocked
    {
        get
        {
            return SkillManager.instance.IsUnlocked(skill);
        }
    }

    private bool isSkillPointAvailable
    {
        get
        {
            return SkillManager.instance.skillPoints > 0;
        }
    }

    public void Initialize(Skill skill)
    {
        this.skill = skill;
        _iconImage.sprite = skill.sprite;
        SkillManager.instance.RegisterSkill(skill);
    }

    public void UnlockSkill()
    {
        SkillManager.instance.Unlock(skill);
    }

    private void SetColor(Skill skill_) => SetColor();

    private void SetColor()
    {
        _bgImage.color = isUnlocked ? _unlockedColor : _lockedColor;
        _canvasGroup.alpha = (isUnlocked || isSkillPointAvailable) ? 1f : _unavailableAlpha;
        _button.interactable = !isUnlocked;
    }
}
