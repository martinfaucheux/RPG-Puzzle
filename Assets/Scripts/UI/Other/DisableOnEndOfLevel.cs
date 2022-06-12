using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnEndOfLevel : MonoBehaviour
{
    void Start()
    {
        GameEvents.instance.onEnterState += OnEnterState;
    }

    void OnDestroy()
    {
        GameEvents.instance.onEnterState -= OnEnterState;
    }

    private void OnEnterState(GameState state)
    {
        if (state == GameState.END_LEVEL)
        {
            gameObject.SetActive(false);
        }
    }
}
