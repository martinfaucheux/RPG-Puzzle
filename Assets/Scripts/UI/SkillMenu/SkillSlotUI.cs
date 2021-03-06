using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    // TODO: This should be moved to SkillManager instead
    public Skill skill;
    [SerializeField] private Image _imageComponent;

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
        _buttonComponent.interactable = false;
        SkillManager.instance.Enable(skill);
    }

    private void SetSprite(){
        _imageComponent.sprite = skill.sprite;
        Color newColor = _imageComponent.color;
        newColor.a = 1;
        _imageComponent.color = newColor;
    }
}
