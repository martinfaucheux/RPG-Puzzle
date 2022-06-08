using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// <summary>
// Debug tool to print which GameObject is getting selected
// </summary>
public class ButtonSelectIndicatorUI : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    // When highlighted with mouse.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Do something.
        Debug.Log("<color=red>Event:</color> Completed mouse highlight.");
    }
    // When selected.
    public void OnSelect(BaseEventData eventData)
    {
        // Do something.
        Debug.Log("<color=red>Event:</color> Completed selection.");
    }
}