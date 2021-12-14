using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAttackStat : UIEntityComponent
{
    public Text menuAttackText;
    private Attack _attackComponent;

    public override void AttachObject(GameObject attachedObject)
    {
        base.AttachObject(attachedObject);
        _attackComponent = attachedObject.GetComponent<Attack>();
        UpdateUI();
    }


    public override void UpdateUI()
    {
        if (IsReady())
        {

            menuAttackText.text = _attackComponent.attackPoints.ToString();
        }
    }

    public bool IsReady()
    {
        return (menuAttackText != null);
    }
}
