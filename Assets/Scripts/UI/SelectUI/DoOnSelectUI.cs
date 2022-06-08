using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// <summary>
// This class is used to do something when a button is selected.
// </summary>
public class DoOnSelectUI : MonoBehaviour, ISelectHandler
{
    [SerializeField] UnityEvent onSelectEvent;

    public void OnSelect(BaseEventData eventData)
    {
        onSelectEvent.Invoke();
    }
}
