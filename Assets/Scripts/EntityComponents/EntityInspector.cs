using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInspector : ClickAndRelease
{
    public string entityName;

    // handle Health, Attack
    private Attack _attackComponent;
    private Health _healthComponent;

    void Start()
    {
        _attackComponent = GetComponent<Attack>();
        _healthComponent = GetComponent<Health>();
    }

    protected override void PerformAction()
    {
        
        // block interaction if any menu is open
        if (
            !GameManager.instance.isGamePaused
            && !SkillMenu.instance.isShowing
            && !MenuController.instance.isOpen
        ){
            MenuController.instance.AttachObject(gameObject);
            MenuController.instance.InstantShowMenu();
        }
    }
}
