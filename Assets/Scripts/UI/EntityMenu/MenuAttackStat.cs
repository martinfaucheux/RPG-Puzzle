using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuAttackStat : UIEntityComponent
{
    public TextMeshProUGUI menuAttackText;
    private Attack _attackComponent;

    public override void AttachObject(GameObject attachedObject)
    {
        base.AttachObject(attachedObject);
        _attackComponent = attachedObject.GetComponent<Attack>();
        if (_attackComponent == null)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            gameObject.SetActive(true);
        }
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
