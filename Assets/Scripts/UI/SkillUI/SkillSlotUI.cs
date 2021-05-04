using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    // TODO: This should be moved to SkillManager instead
    public Skill skill;
    [SerializeField] private Image _imageComponent;

    [Tooltip("Display or not the skill information with SkillDescriptionUpdater")]
    public bool updateDescriptionOnHover = true;

    private Button _buttonComponent;

    void Start()
    {
        if (skill != null){
            SkillManager.instance.AddSkill(skill);
            SetSprite();
        }

        _buttonComponent = GetComponent<Button>();
        
    }

    public void Enable(){
        bool result = SkillManager.instance.Enable(skill);
        if (result){
            _buttonComponent.interactable = false;
        }
        else SkillCounterWizzer.instance.Wizz();
    }

    private void SetSprite(){
        _imageComponent.sprite = skill.sprite;
        Color newColor = _imageComponent.color;
        newColor.a = 1;
        _imageComponent.color = newColor;
    }

    public void SetSkillDescription(){
        SkillDescriptionUpdater.instance.SetSkillInformation(skill);
    }
    public static void CleanSkillDescription(){
        SkillDescriptionUpdater.instance.CleanSkillInformation();
    }
}
