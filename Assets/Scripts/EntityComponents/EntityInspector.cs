using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInspector : MonoBehaviour
{
    public string entityName;

    // handle Health, Attack
    private Attack _attackComponent;
    private Health _healthComponent;

    // Start is called before the first frame update
    void Start()
    {
        _attackComponent = GetComponent<Attack>();
        _healthComponent = GetComponent<Health>();
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MenuController.instance.AttachObject(gameObject);
            MenuController.instance.Trigger();
        }
        UIManager.instance.UpdateUI();
    }
}
