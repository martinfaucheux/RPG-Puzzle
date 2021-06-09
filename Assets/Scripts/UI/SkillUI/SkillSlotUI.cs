using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : SelectButton
{
    // TODO: This should be moved to SkillManager instead
    public Skill skill;
    public SkillSelectorUI skillSelectorUI{private get; set;}
    [SerializeField] Image _skillIconImage;
    [SerializeField] Image _highlightImage;
    [SerializeField] Image _borderImage;
    [SerializeField] float stateTransitionDuration = 0.5f;


    void Start()
    {
        if (skill != null){
            SkillManager.instance.RegisterSkill(skill);
            SetSprite();
        }
        GameEvents.instance.onSkillEnabled += OnUnlockSkill;
        // fix some issues when skills keep selected
        // after opening / closing skill menu
        GameEvents.instance.onCloseSkillMenu += Unselect;
    }

    public void SetSprite(){
        _skillIconImage.sprite = skill.sprite;
        Color newColor = _skillIconImage.color;
        newColor.a = 1;
        _skillIconImage.color = newColor;
    }

    protected override void OnButtonSelect(){
        skillSelectorUI?.SelectSkill(skill);
        ScaleHighlightImage(1.1f);
    }
    protected override void OnButtonUnselect(){
        ScaleHighlightImage(1f);
        // clear the skill description if no skill are selected
        if (currentSelected == null){
            skillSelectorUI.ClearSkill();
        }
    }

    private void OnUnlockSkill(Skill skill){
        if (skill == this.skill){
            FadeOutButton();
        }
    }

    private void FadeOutButton(){
        // Called when the skill is already unlocked
        float targetAlphaValue = 0.5f;
        LeanTween.alpha((RectTransform) _skillIconImage.transform, targetAlphaValue, stateTransitionDuration).setEaseOutQuint();
        LeanTween.alpha((RectTransform) _borderImage.transform, targetAlphaValue, stateTransitionDuration).setEaseOutQuint();
        LeanTween.alpha((RectTransform) _highlightImage.transform, 0f, stateTransitionDuration).setEaseOutQuint();
    }


    private void ScaleHighlightImage(float newScale){
        Vector3 _newScale = newScale * new Vector3(1, 1, 0);
        LeanTween.scale(_highlightImage.gameObject, _newScale, stateTransitionDuration);
    }

    void OnDestroy(){
        GameEvents.instance.onSkillEnabled -= OnUnlockSkill;
        GameEvents.instance.onCloseSkillMenu -= Unselect;
    }
}
