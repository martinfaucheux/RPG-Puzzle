using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHealthStat : UIEntityComponent
{
    public Text menuHealthText;

    private Health _healthComponent;


    public override void AttachObject(GameObject attachedObject)
    {
        base.AttachObject(attachedObject);
        _healthComponent = attachedObject.GetComponent<Health>();
        UpdateUI();
    }


    public override void UpdateUI()
    {
        if (IsReady())
        {
            int currentHealth = _healthComponent.CurrentHealthPoints;
            int maxHealth = _healthComponent.MaxHealthPoints;

            menuHealthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    public bool IsReady()
    {
        return (_healthComponent != null);
    }
}
