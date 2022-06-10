using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EntityInspector : MonoBehaviour
{
    public string entityName;

    [TextArea]
    public string description;


    private bool canShow
    {
        get
        {
            return !GameManager.instance.isGamePaused
            && !SkillMenu.instance.isShowing;
        }
    }

    void OnMouseEnter()
    {
        if (canShow)
        {
            ContextMenuController.instance.Show(gameObject);
        }
    }

    void OnMouseExit()
    {
        ContextMenuController.instance.Hide();
    }
}
