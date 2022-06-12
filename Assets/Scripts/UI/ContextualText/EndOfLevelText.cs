using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndOfLevelText : BlinkingText
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
        if (state == GameState.WIN)
        {
            DelayShow();
        }
    }
}