using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuDescription : UIEntityComponent
{
    [SerializeField] TextMeshProUGUI entityDescriptionText;

    private EntityInspector _entityInspectorComponent;

    protected override void Start()
    {
        base.Start();
        entityDescriptionText.text = "";
    }

    public override void AttachObject(GameObject attachedObject)
    {
        base.AttachObject(attachedObject);
        _entityInspectorComponent = attachedObject.GetComponent<EntityInspector>();
        if (_entityInspectorComponent.description == "")
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
            entityDescriptionText.text = _entityInspectorComponent.description;
        }
    }

    public bool IsReady()
    {
        return (_entityInspectorComponent != null);
    }
}
