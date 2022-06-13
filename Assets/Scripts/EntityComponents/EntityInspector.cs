using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EntityInspector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string entityName;
    [TextArea]
    [SerializeField] string description;
    [SerializeField] Health _health;
    [SerializeField] Attack _attack;
    private float timeBeforeShow = 0.2f;
    private float _hoverDuration = -1;

    public string GetName() => entityName;
    public string SetName(string value) => entityName = value;
    public string SetDescription(string value) => description = value;

    private bool canShow { get { return StateManager.instance.currentGameState == GameState.PLAY; } }

    void Update()
    {
        if (_hoverDuration >= 0)
        {
            _hoverDuration += Time.deltaTime;
            if (_hoverDuration > timeBeforeShow)
            {
                _hoverDuration = -1;
                InspectorData inspectorData = new InspectorData(entityName, description, _health, _attack);
                ContextMenuController.instance.Show(gameObject, inspectorData);
            }
        }
    }

    void OnMouseEnter()
    {
        if (canShow)
        {
            _hoverDuration = 0;
        }
    }

    void OnMouseExit()
    {
        _hoverDuration = -1;
        ContextMenuController.instance.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canShow)
        {
            _hoverDuration = 0;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverDuration = -1;
        ContextMenuController.instance.Hide();
    }
}
