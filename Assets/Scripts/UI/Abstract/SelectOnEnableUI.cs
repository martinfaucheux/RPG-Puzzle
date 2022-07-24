using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// <summary>
// Select this GamObject using the EventSystem when the object is enabled
// </summary>
public class SelectOnEnableUI : MonoBehaviour
{
    [SerializeField] private float delay = 0.05f;
    void OnEnable()
    {
        if (delay <= 0f)
        {
            Select();
        }
        else
        {
            StartCoroutine(SelectCoroutine());
        }
    }

    private void Select()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    private IEnumerator SelectCoroutine()
    {
        // yield return new WaitForSeconds(delay);
        yield return new WaitForEndOfFrame();
        Select();
    }

}