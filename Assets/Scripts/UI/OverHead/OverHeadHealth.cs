using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverHeadHealth : MonoBehaviour
{
    public RectTransform healthBarRectTransform;
    public RectTransform fillImageRectTransform;
    public bool isShowing = false;

    private Health _healthComponent;
    private List<Image> _imageList;

    void Start()
    {

        if (fillImageRectTransform == null | healthBarRectTransform == null)
        {
            Debug.LogError(gameObject.ToString() + ": fill image TectTransform not found");
        }

        _healthComponent = GetComponentInParent<Health>();
        if (_healthComponent == null)
        {
            Debug.LogError(gameObject.ToString() + ": Cannot find Health Component in parents");
        }
        else
        {
            GameEvents.instance.onHealthChange += OnHealthChange;
        }

        // add the 3 UI images here in the list
        _imageList = new List<Image>
        {
            GetComponent<Image>(),
            healthBarRectTransform.GetComponent<Image>(),
            fillImageRectTransform.GetComponent<Image>()
        };
        Hide();
    }

    // TODO: To Enable

    public void Toggle()
    {
        if (isShowing)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Hide()
    {
        // for each image in the list disable it
        foreach (Image image in _imageList)
        {
            image.enabled = false;
        }
        isShowing = false;
    }

    public void Show()
    {
        // for each image in the list enable it
        foreach (Image image in _imageList)
        {
            image.enabled = true;
        }
        isShowing = true;
    }

    private void OnHealthChange(int healthID, int initValue, int finalValue)
    {
        if (healthID == _healthComponent.GetInstanceID())
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        float healthPercentage = (float)_healthComponent.CurrentHealthPoints / (float)_healthComponent.MaxHealthPoints;

        float barMaxWidth = healthBarRectTransform.sizeDelta.x;
        float fillWidth = barMaxWidth * healthPercentage;

        fillImageRectTransform.sizeDelta = new Vector2(fillWidth, 0f);
    }
}