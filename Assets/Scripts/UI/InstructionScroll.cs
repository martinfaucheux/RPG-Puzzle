using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionScroll : MonoBehaviour
{
    [SerializeField] private bool showOnLevelLoaded;
    [SerializeField] private TextMeshProUGUI textComponent;

    void Start()
    {
        if (showOnLevelLoaded)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Show()
    {
        foreach (Transform childTransform in transform)
        {
            transform.gameObject.SetActive(true);
        }
        GameManager.instance.isInstruction = true;
    }
    public void Hide()
    {
        foreach (Transform childTransform in transform)
        {
            transform.gameObject.SetActive(false);
        }
        GameManager.instance.isInstruction = false;
    }

}
