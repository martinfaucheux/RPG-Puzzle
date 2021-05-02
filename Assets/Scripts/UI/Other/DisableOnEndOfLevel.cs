using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnEndOfLevel : MonoBehaviour
{
    void Start()
    {
        GameEvents.instance.onEndOfLevel += Trigger;
    }

    void OnDestroy()
    {
        GameEvents.instance.onEndOfLevel -= Trigger;
    }

    private void Trigger()
    {
        gameObject.SetActive(false);
    }
}
