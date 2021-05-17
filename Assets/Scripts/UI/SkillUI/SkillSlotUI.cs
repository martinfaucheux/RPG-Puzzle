using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    // TODO: This should be moved to SkillManager instead
    public Skill skill;
    [SerializeField] private Image _imageComponent;
    [SerializeField] SkillSelectorUI _skillSelectorUI;
    [SerializeField] bool _disableButtonOnUnlock;

    private Button _buttonComponent;

    void Start()
    {
        if (skill != null){
            SkillManager.instance.AddSkill(skill);
            SetSprite();
        }

        _buttonComponent = GetComponent<Button>();

        GameEvents.instance.onSkillEnabled += OnUnlockSkill;
        
    }

    private void SetSprite(){
        _imageComponent.sprite = skill.sprite;
        Color newColor = _imageComponent.color;
        newColor.a = 1;
        _imageComponent.color = newColor;
    }

    public void SelectSkill(){
        _skillSelectorUI?.SelectSkill(skill);
    }

    private void OnUnlockSkill(Skill skill){

        Debug.Log(skill == this.skill);

        if (skill == this.skill && _disableButtonOnUnlock){
            _buttonComponent.interactable = false;
        }
    }

    void OnDestroy(){
        GameEvents.instance.onSkillEnabled -= OnUnlockSkill;
    }
}
