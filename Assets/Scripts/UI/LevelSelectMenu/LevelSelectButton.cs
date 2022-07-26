using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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
        if (levelId == 1)
        {
            StartCoroutine(SelectCoroutine());
        }
    }

    private void SetText()
    {
        textComponent.text = levelId.ToString();
    }

    public void SelectLevel()
    {
        GetComponentInParent<LevelGridManager>().SelectLevel(levelId);
    }

    public void UnselectLevel()
    {
        GetComponentInParent<LevelGridManager>().SelectLevel(0);
    }

    public void LoadSelectedLevel()
    {
        GetComponentInParent<LevelGridManager>().LoadSelectedLevel();
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

    private IEnumerator SelectCoroutine()
    {
        // yield return new WaitForSeconds(delay);
        yield return new WaitForSeconds(0.05f);
        EventSystem.current.SetSelectedGameObject(gameObject);

    }
}
