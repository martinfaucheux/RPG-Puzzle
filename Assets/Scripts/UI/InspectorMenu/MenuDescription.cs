using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuDescription : UIEntityComponent
{
    [SerializeField] TextMeshProUGUI _entityDescriptionText;

    protected override bool ShouldDisplay(InspectorData inspectorData)
    {
        return inspectorData.description != "";
    }

    protected override void UpdateUI(InspectorData inspectorData)
    {
        _entityDescriptionText.text = inspectorData.description;
    }
}
