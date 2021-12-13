using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlatformTextAdaptor : MonoBehaviour
{
    public string desktopVersion;
    public string mobileVersion;

    void Start()
    {
        bool isMobile = (Application.platform == RuntimePlatform.Android);
        string text = isMobile ? mobileVersion : desktopVersion;

        Text textComponent = GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = text;
        }

        TextMeshProUGUI tmProText = GetComponent<TextMeshProUGUI>();
        if (tmProText != null)
        {
            tmProText.text = text;
        }
    }
}
