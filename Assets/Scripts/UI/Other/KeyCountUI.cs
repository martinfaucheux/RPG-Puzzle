using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCountUI : MonoBehaviour
{
    public float slideSpeed = 400;
    private int _previousCount = 0;
    private Vector2 _visiblePos;
    private Vector2 _hiddenPos;
    private RectTransform _rectTransform;
    
    private Inventory _inventory;
    private Text _textComponent;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _textComponent = GetComponentInChildren<Text>();
        _visiblePos = _rectTransform.anchoredPosition;
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        SetHiddenPosition();
        _rectTransform.anchoredPosition = _hiddenPos;
        UpdateKeyCountText(0);

        GameEvents.instance.onPickItem += CheckUpdate;
        GameEvents.instance.onUseItem += CheckUpdate;
    }

    void OnDestroy(){
        GameEvents.instance.onPickItem -= CheckUpdate;
        GameEvents.instance.onUseItem -= CheckUpdate;
    }

    private void CheckUpdate(Item item){
        if(item.GetType() == typeof(Key)){

            int newKeyCount = _inventory.keyCount;

            if (_previousCount != newKeyCount){
                UpdateKeyCountText(newKeyCount);

                if (_previousCount == 0 && newKeyCount > 0){
                    SlideIn();
                }

                else if (newKeyCount == 0 && _previousCount > 0){
                    SlideOut();
                }
                _previousCount = newKeyCount;
            }
        }        
    }

    private void UpdateKeyCountText(int newCount){
        _textComponent.text = "x " + newCount.ToString();
    }

    private void SlideIn(){
        StartCoroutine(SlideCoroutine(_visiblePos));
    }

    private void SlideOut(){
        StartCoroutine(SlideCoroutine(_hiddenPos));
    }

    private IEnumerator SlideCoroutine(Vector2 endPos){

        while((endPos - _rectTransform.anchoredPosition).magnitude > 0.1){
            Vector2 directionVect = (endPos - _rectTransform.anchoredPosition).normalized;
            Vector2 newPos = _rectTransform.anchoredPosition + directionVect * slideSpeed * Time.deltaTime;

            _rectTransform.anchoredPosition = newPos;
            yield return null;
        }
        _rectTransform.anchoredPosition = endPos;
    }

    private void SetHiddenPosition(){
        RectTransform parentRectTransform = gameObject.transform.parent.GetComponent<RectTransform>();
        float slideDistance = parentRectTransform.sizeDelta.x;
        _hiddenPos = _visiblePos + new Vector2(-slideDistance, 0f);
    }
}
