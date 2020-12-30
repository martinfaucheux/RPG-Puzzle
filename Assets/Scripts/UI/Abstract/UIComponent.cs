using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIComponent : MonoBehaviour
{
    public static List<UIComponent> registeredUIComponents;

    public void Register()
    {
        registeredUIComponents.Add(this);
    }

    protected virtual void Start()
    {
        if (registeredUIComponents == null)
        {
            registeredUIComponents = new List<UIComponent>();
        }
        Register();
    }

    public void OnDestroy()
    {
        if (registeredUIComponents != null)
        {
            registeredUIComponents.Remove(this);
        }
    }

    public abstract void UpdateUI();
}
