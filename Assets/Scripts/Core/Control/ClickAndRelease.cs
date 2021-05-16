using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClickAndRelease : MonoBehaviour
{
    [SerializeField] int buttonId = 0;

    // whether the current object is currently hovered
    private bool _isHovered = false;
    // whether the current object has been clicked on
    private bool _isClicked = false;

    void Update(){

        // mark as clicked
        if (Input.GetMouseButtonDown(buttonId))
        {
            if(_isHovered && !_isClicked){
                _isClicked = true;
            }
        }

        // perform action if the object is still hovered
        // while button is released
        if (Input.GetMouseButtonUp(buttonId)){
            if(_isHovered && _isClicked){
                PerformAction();
            }
            // unclick
            _isClicked = false;
        }
    }

    void OnMouseEnter(){
        _isHovered = true;
    }

    void OnMouseExit(){
        _isHovered = false;
    }

    // to be overriden
    protected abstract void PerformAction();

}
