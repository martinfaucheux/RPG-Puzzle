﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuEntityName : UIEntityComponent
{
    public TextMeshProUGUI entityNameText;

    private EntityInspector _entityInspectorComponent;

    public override void AttachObject(GameObject attachedObject)
    {
        base.AttachObject(attachedObject);
        _entityInspectorComponent = attachedObject.GetComponent<EntityInspector>();
        UpdateUI();
    }

    public override void UpdateUI()
    {
        if (IsReady())
        {
            entityNameText.text = _entityInspectorComponent.entityName;
        }
    }

    public bool IsReady()
    {
        return (_entityInspectorComponent != null);
    }
}
