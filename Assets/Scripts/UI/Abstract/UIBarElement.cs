using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBarElement : MonoBehaviour
{
    public Image imageComponent = null;

    public abstract void SetValue(bool boolValue);

    public void Hide()
    {
        imageComponent.enabled = false;
    }

    public void Show()
    {
        imageComponent.enabled = true;
    }
}
