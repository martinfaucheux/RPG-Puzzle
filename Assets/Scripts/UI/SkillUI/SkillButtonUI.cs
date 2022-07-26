using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour
{
    [SerializeField] float _unavailableAlpha = 0.5f;
    [SerializeField] float _highlightDuration = 4f;
    [SerializeField] float _highlightPeriod = 8f;
    [SerializeField] Color _unlockedColor;
    [SerializeField] Color _lockedColor;
    [SerializeField] Color _inspectorShortcutColor;
    [SerializeField] Image _iconImage;
    [SerializeField] Image _bgImage;
    [SerializeField] Image _highlightImage;
    [SerializeField] Button _button;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] EntityInspector _entityInspector;
    [SerializeField] Material _availableMaterial;

    private Skill skill;
    private float _lastShineTime;

    public int skillId { get { return transform.GetSiblingIndex(); } }

    private static string[] _shortcuts = new string[] { "&", "é", "\"" };

    void Start()
    {
        SetEntityInspectorName();
        UpdateUI();

        GameEvents.instance.onSkillEnabled += UpdateUI;
        GameEvents.instance.onLevelUp += UpdateUI;
    }

    void OnDestroy()
    {
        GameEvents.instance.onSkillEnabled -= UpdateUI;
        GameEvents.instance.onLevelUp -= UpdateUI;
    }

    void Update()
    {
        if (Time.time - _lastShineTime > _highlightPeriod)
        {
            Shine();
        }
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
        _highlightImage.enabled = isSkillPointAvailable && !isUnlocked;
        _canvasGroup.alpha = (isUnlocked || isSkillPointAvailable) ? 1f : _unavailableAlpha;
        _button.interactable = !isUnlocked;

        Material material = (!isUnlocked && isSkillPointAvailable) ? _availableMaterial : null;
        _bgImage.material = material;
        _iconImage.material = material;
        SetEntityInspectorDescription();
        Shine();
    }

    private void SetEntityInspectorName()
    {
        string shortcut = PlayInputManager.instance.GetSkillBindingPath(skillId);
        string colorHex = ColorUtility.ToHtmlStringRGB(_inspectorShortcutColor);
        string inspectorName = $"{skill.skillName} <#{colorHex}>[{shortcut}]";
        _entityInspector.SetName(inspectorName);
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

    public void Shine()
    {
        _lastShineTime = Time.time;

        // reset state
        LeanTween.cancel(_highlightImage.gameObject);
        _highlightImage.transform.localScale = Vector3.one;
        _highlightImage.color = Color.white;

        float scaleFactor = 0.5f;
        Vector3 targetScale = Vector3.one + scaleFactor * new Vector3(1f, 1f, 0f);
        LeanTween.scale(_highlightImage.gameObject, targetScale, _highlightDuration).setEase(LeanTweenType.easeOutCubic);
        LeanTween.alpha((RectTransform)_highlightImage.transform, 0f, _highlightDuration).setEase(LeanTweenType.easeInCubic);
    }
}
