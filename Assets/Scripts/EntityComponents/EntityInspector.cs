using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EntityInspector : MonoBehaviour
{
    public string entityName;
    [TextArea]
    public string description;

    private float timeBeforeShow = 0.5f;

    private float _hoverDuration = -1;



    private bool canShow
    {
        get
        {
            return !GameManager.instance.isGamePaused
            && !SkillMenu.instance.isShowing;
        }
    }

    void Update()
    {
        if (_hoverDuration >= 0)
        {
            _hoverDuration += Time.deltaTime;
            if (_hoverDuration > timeBeforeShow)
            {
                _hoverDuration = -1;
                ContextMenuController.instance.Show(gameObject);
            }
        }
    }

    void OnMouseEnter()
    {
        if (canShow)
        {
            _hoverDuration = 0;
        }
    }

    void OnMouseExit()
    {
        _hoverDuration = -1;
        ContextMenuController.instance.Hide();
    }
}
