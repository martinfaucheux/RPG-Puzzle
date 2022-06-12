using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEnterActionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UnityEvent onPointerEnterEvent;
    [SerializeField] UnityEvent onPointerExitEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnterEvent.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExitEvent.Invoke();
    }
}
