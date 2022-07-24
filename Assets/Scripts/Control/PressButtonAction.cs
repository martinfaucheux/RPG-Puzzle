using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressButtonAction : MonoBehaviour
{
    [SerializeField] KeyCode key;
    [SerializeField] bool anyKey;
    [SerializeField] UnityEvent onEscapePressedEvent;

#if ENABLE_LEGACY_INPUT_MANAGER 
    void Update()
    {
        if (
            (anyKey && Input.anyKeyDown)
            || Input.GetKeyDown(key)
        )
        {
            onEscapePressedEvent.Invoke();
        }
    }
#endif
}
