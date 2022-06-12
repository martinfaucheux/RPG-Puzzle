using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIEntityComponent : MonoBehaviour
{
    private GameObject _attachedGameObject;

    public void AttachObject(GameObject attachedGameObject, InspectorData inspectorData)
    {
        this._attachedGameObject = attachedGameObject;
        if (ShouldDisplay(inspectorData))
        {
            gameObject.SetActive(true);
            UpdateUI(inspectorData);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual bool ShouldDisplay(InspectorData inspectorData)
    {
        return true;
    }

    protected abstract void UpdateUI(InspectorData inspectorData);

}
