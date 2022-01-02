using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionScroll : MonoBehaviour
{
    [SerializeField] private float showDelay = 0.5f;
    [SerializeField] private bool showOnLevelLoaded;

    void Start()
    {
        if (showOnLevelLoaded)
        {
            DelayShow();
        }
        else
        {
            Hide();
        }
    }

    public void DelayShow()
    {
        StartCoroutine(ShowCoroutine());
    }

    public void Hide()
    {
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(false);
        }
        GameManager.instance.isInstruction = false;
    }

    private IEnumerator ShowCoroutine()
    {
        GameManager.instance.isInstruction = true;
        yield return new WaitForSecondsRealtime(showDelay);
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(true);
        }
    }
}
