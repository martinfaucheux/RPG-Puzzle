using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHealthBar : UIEntityComponent
{
    private Health _healthComponent;

    public RectTransform healthBarRectTransform;
    public RectTransform fillImageRectTransform;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if (fillImageRectTransform == null | healthBarRectTransform == null)
        {
            Debug.LogError(gameObject.ToString() + ": fill image TectTransform not found");
        }
    }

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
            float barMaxWidth = healthBarRectTransform.sizeDelta.x;
            float barHeight = healthBarRectTransform.sizeDelta.y;

            float healthPercentage = _healthComponent.GetHealthPercentage();
            float fillWidth = barMaxWidth * healthPercentage;

            fillImageRectTransform.sizeDelta = new Vector2(fillWidth, barHeight);
        }
    }

    public bool IsReady()
    {
        return (healthBarRectTransform != null) && (fillImageRectTransform != null) && (_healthComponent != null);
    }
}