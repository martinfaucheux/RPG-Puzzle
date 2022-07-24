using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// Implement `PerformAction` to handle the action when the user clicks then releases on the object.
// </summary>
public abstract class ClickAndRelease : MonoBehaviour
{
    [SerializeField] int buttonId = 0;

    // whether the current object is currently hovered
    private bool _isHovered = false;
    // whether the current object has been clicked on
    private bool _isClicked = false;

#if ENABLE_LEGACY_INPUT_MANAGER 

    void Update()
    {


        // mark as clicked
        if (Input.GetMouseButtonDown(buttonId))
        {
            if (_isHovered && !_isClicked)
            {
                _isClicked = true;
            }
        }

        // perform action if the object is still hovered
        // while button is released
        if (Input.GetMouseButtonUp(buttonId))
        {
            if (_isHovered && _isClicked)
            {
                PerformAction();
            }
            // unclick
            _isClicked = false;
        }
    }

#endif

    void OnMouseEnter()
    {
        _isHovered = true;
    }

    void OnMouseExit()
    {
        _isHovered = false;
    }

    // to be overriden
    protected abstract void PerformAction();

}
