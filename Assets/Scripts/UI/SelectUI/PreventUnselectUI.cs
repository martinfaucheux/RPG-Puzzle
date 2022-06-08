using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// <summary>
// Prevent from unselecting any GameObject by clicking on the background
// </summary>
public class PreventUnselectUI : MonoBehaviour
{
    private EventSystem evt;
    private GameObject selectedGO;

    private void Start()
    {
        evt = EventSystem.current;
    }

    private void Update()
    {
        if (
            evt.currentSelectedGameObject != null
            && evt.currentSelectedGameObject != selectedGO
        )
            selectedGO = evt.currentSelectedGameObject;
        else if (
            selectedGO != null
            && evt.currentSelectedGameObject == null
            && !evt.alreadySelecting
        )
            evt.SetSelectedGameObject(selectedGO);
    }
}