using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// <summary>
// Script used to control level select buttons
// - determine the button appearance
// - determine if the button must be selected on scene load
// - hold onClick and onSelect events
// - hold a reference to the LevelGridManager
// </summary>
public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] Image backgroundImage;
    [SerializeField] Image reflectionImage;
    [SerializeField] Button buttonComponent;
    [SerializeField] float inactiveBgAlpha = 0.5f;
    [Tooltip("Used to select the first level on scene load.")]
    [SerializeField] SelectOnEnableUI selectOnEnableUI;

    private LevelGridManager _levelGridManager;

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
        _levelGridManager = GetComponentInParent<LevelGridManager>();
        levelId = transform.GetSiblingIndex() + 1;

        // Select the first level
        selectOnEnableUI.enabled = (levelId == 1);
    }

    private void SetText()
    {
        textComponent.text = levelId.ToString();
    }

    public void SelectLevel()
    {
        _levelGridManager.SelectLevel(levelId);
    }

    public void LoadSelectedLevel()
    {
        _levelGridManager.LoadSelectedLevel();
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
