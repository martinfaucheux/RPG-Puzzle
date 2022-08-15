using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoOnStateChange : MonoBehaviour
{
    [SerializeField] string stateName;
    [SerializeField] UnityEvent actionToPerform;

    private GameState _state;

    void Start()
    {
        _state = GameState.GetByName(stateName);
        GameEvents.instance.onEnterState += PerformAction;
    }

    void PerformAction(GameState state)
    {
        if (state == this._state)
            actionToPerform.Invoke();
    }


}
