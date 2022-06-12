using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuAttackStat : UIEntityComponent
{
    [SerializeField] TextMeshProUGUI _menuAttackText;

    protected override bool ShouldDisplay(InspectorData inspectorData)
    {
        return inspectorData.attack != null;
    }

    protected override void UpdateUI(InspectorData inspectorData)
    {
        _menuAttackText.text = inspectorData.attack.attackPoints.ToString();
    }
}
