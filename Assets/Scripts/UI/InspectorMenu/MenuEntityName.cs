using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuEntityName : UIEntityComponent
{
    [SerializeField] TextMeshProUGUI _entityNameText;

    protected override void UpdateUI(InspectorData inspectorData)
    {
        _entityNameText.text = inspectorData.entityName;
    }
}
