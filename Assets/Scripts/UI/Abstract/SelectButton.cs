using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour, IPointerClickHandler // required interface for OnSelect
{
    private static List<SelectButton> _selectButtons;
    public static SelectButton currentSelected{
        get; private set;
    }

    public bool selected{ get; private set;}

    void Awake(){
        if (_selectButtons == null){
            _selectButtons = new List<SelectButton>{this};
        }
        else{
            _selectButtons.Add(this);
        }
    }

    public void OnPointerClick(PointerEventData eventData) => HandleEvent();

    private void HandleEvent(){
        UnselectAll();
        Select();        
    }

    protected virtual void OnButtonSelect(){}
    protected virtual void OnButtonUnselect(){}

    private void UnselectAll(bool ignoreThis = true){
        foreach(SelectButton button in _selectButtons){
            if (!(button == this && !ignoreThis)){
                button.Unselect();
            }
        }
    }

    protected void Select(){
        if (!selected){
            selected = true;
            currentSelected = this;
            OnButtonSelect();
        }
    }

    protected void Unselect(){
        if (selected){
            currentSelected = null;
            selected = false;
            OnButtonUnselect();
        }
    }
}
