using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInspector : MonoBehaviour
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

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
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
        UIManager.instance.UpdateUI();
    }
}
