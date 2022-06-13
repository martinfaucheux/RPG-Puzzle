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
    [SerializeField] EntityInspector _entityInspector;
    [SerializeField] Material _availableMaterial;

    private Skill skill;

    void Start()
    {
        _entityInspector.SetName(skill.skillName);
        UpdateUI();

        GameEvents.instance.onSkillEnabled += UpdateUI;
        GameEvents.instance.onLevelUp += UpdateUI;
    }

    void OnDestroy()
    {
        GameEvents.instance.onSkillEnabled -= UpdateUI;
        GameEvents.instance.onLevelUp -= UpdateUI;
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
        if (StateManager.instance.currentGameState == GameState.PLAY)
            SkillManager.instance.Unlock(skill);
    }

    private void UpdateUI(Skill skill_) => UpdateUI();

    private void UpdateUI()
    {
        _bgImage.color = isUnlocked ? _unlockedColor : _lockedColor;
        _canvasGroup.alpha = (isUnlocked || isSkillPointAvailable) ? 1f : _unavailableAlpha;
        _button.interactable = !isUnlocked;

        Material material = (!isUnlocked && isSkillPointAvailable) ? _availableMaterial : null;
        _bgImage.material = material;
        _iconImage.material = material;
        SetEntityInspectorDescription();
    }

    private void SetEntityInspectorDescription()
    {
        string description = skill.skillDescription;
        if (!isUnlocked)
        {
            if (isSkillPointAvailable)
            {
                description += "\n<i>Click to unlock</i>";
            }
            else
            {
                description += "\n<i>No skill point to spend</i>";
            }
        }
        _entityInspector.SetDescription(description);
    }
}
