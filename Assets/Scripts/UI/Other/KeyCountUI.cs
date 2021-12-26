using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCountUI : MonoBehaviour
{
    [SerializeField] float slideDuration = 0.2f;
    [SerializeField] float distanceToEdge = 0;
    private int _previousCount = 0;
    private Vector2 _visiblePos;
    private Vector2 _hiddenPos;
    private RectTransform _rectTransform;

    private Inventory _inventory;
    private Text _textComponent;

    void Start()
    {
        _rectTransform = (RectTransform)transform;
        _textComponent = GetComponentInChildren<Text>();
        _visiblePos = _rectTransform.anchoredPosition;
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        SetHiddenPosition();
        _rectTransform.anchoredPosition = _hiddenPos;
        UpdateKeyCountText(0);

        GameEvents.instance.onPickItem += CheckUpdate;
        GameEvents.instance.onUseItem += CheckUpdate;
    }

    void OnDestroy()
    {
        GameEvents.instance.onPickItem -= CheckUpdate;
        GameEvents.instance.onUseItem -= CheckUpdate;
    }

    private void CheckUpdate(Item item)
    {
        if (item.GetType() == typeof(Key))
        {

            int newKeyCount = _inventory.keyCount;

            if (_previousCount != newKeyCount)
            {
                UpdateKeyCountText(newKeyCount);

                if (_previousCount == 0 && newKeyCount > 0)
                {
                    SlideIn();
                }

                else if (newKeyCount == 0 && _previousCount > 0)
                {
                    SlideOut();
                }
                _previousCount = newKeyCount;
            }
        }
    }

    private void UpdateKeyCountText(int newCount)
    {
        _textComponent.text = "x " + newCount.ToString();
    }

    private void SlideIn()
    {
        LeanTween.move(_rectTransform, _visiblePos, slideDuration);
    }

    private void SlideOut()
    {
        LeanTween.move(_rectTransform, _hiddenPos, slideDuration);
    }

    private void SetHiddenPosition()
    {
        RectTransform parentRectTransform = (RectTransform)transform.parent.transform;

        // Not really sure about this part


        float slideDistance = parentRectTransform.sizeDelta.x + distanceToEdge;
        _hiddenPos = _visiblePos + new Vector2(-slideDistance, 0f);
    }

}
