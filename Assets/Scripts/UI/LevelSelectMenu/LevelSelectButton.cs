using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] Image backgroundImage;
    [SerializeField] Image reflectionImage;
    [SerializeField] Button buttonComponent;
    [SerializeField] float inactiveBgAlpha = 0.5f;

    private int _levelId;
    public int levelId
    {
        get { return _levelId; }
        set
        {
            _levelId = value;
            SetText();
        }
    }

    void Start()
    {
        levelId = transform.GetSiblingIndex() + 1;
    }

    private void SetText()
    {
        textComponent.text = levelId.ToString();
    }

    public void SelectLevel()
    {
        GetComponentInParent<LevelGridManager>().SelectLevel(levelId);
    }

    public void SetBackgroundColor(Color color)
    {
        // backgroundImage.color = color;
        ColorBlock colorBlock = buttonComponent.colors;
        colorBlock.normalColor = color;
        buttonComponent.colors = colorBlock;
    }

    public void SetReflectionColor(Color color)
    {
        reflectionImage.color = color;
    }

    public void SetButtonActive(bool isActive)
    {
        buttonComponent.interactable = isActive;
        reflectionImage.enabled = isActive;

        Color bgColor = backgroundImage.color;
        bgColor.a = isActive ? 1f : inactiveBgAlpha;
        backgroundImage.color = bgColor;
    }
}
