using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingUI : MonoBehaviour
{
    public enum EnterDirection
    {
        TOP,
        BOTTOM,
        LEFT,
        RIGHT,
    }

    public EnterDirection enterDirection;
    public float slideUnits = 400f;
    public float translationDuration = 1f;

    public bool autoGetHiddenPosition = false;

    private RectTransform _rectTransform;
    private RectTransform _canvasRectTransform;

    private Vector2 _showPosition;
    private Vector2 _hidePosition;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasRectTransform = GetComponentInParent<Canvas>().gameObject.GetComponent<RectTransform>();
        _showPosition = _rectTransform.anchoredPosition;

        if (autoGetHiddenPosition){
            _hidePosition = GetStartPos();
        } else {
            _hidePosition = _showPosition + GetTranslation();
        }

        _rectTransform.anchoredPosition = _hidePosition;
    }

    public void SlideIn() {
        StartCoroutine(SlideCoroutine(_hidePosition, _showPosition, translationDuration));
    }

    public void SlideOut()
    {
        StartCoroutine(SlideCoroutine(_rectTransform.anchoredPosition, _hidePosition, translationDuration));
    }

    private Vector2 GetTranslation()
    {
        Vector2 directionVector = Vector2.zero;
        switch (enterDirection)
        {
            case EnterDirection.BOTTOM:
                directionVector = Vector2.down;
                break;
            case EnterDirection.TOP:
                directionVector = Vector2.up;
                break;
            case EnterDirection.LEFT:
                directionVector = Vector2.left;
                break;
            case EnterDirection.RIGHT:
                directionVector = Vector2.right;
                break;
        }
        return slideUnits * directionVector;
    }

    private Vector3 GetStartPos(){
        float canvasWidth = _canvasRectTransform.rect.width;
        float canvasHeight = _canvasRectTransform.rect.height;
        float objectWidth = _rectTransform.rect.width;
        float objectHeight = _rectTransform.rect.height;
        float initX = _rectTransform.anchoredPosition.x;
        float initY = _rectTransform.anchoredPosition.y;

        Vector3 position;
        switch(enterDirection){
            case EnterDirection.BOTTOM:
                position = new Vector3(initX, -(canvasHeight + objectHeight) / 2f, 0f);;
                break;
            case EnterDirection.TOP:
                position = new Vector3(initX, (canvasHeight + objectHeight) / 2f, 0f);;
                break;
            case EnterDirection.LEFT:
                position = new Vector3(-(canvasWidth + objectWidth) / 2f, initY, 0f);;
                break;
            case EnterDirection.RIGHT:
                position = new Vector3((canvasWidth + objectWidth) / 2f, initY, 0f);;
                break;
            default:
                position = Vector3.zero;
                break;
        }

        return position;
    }

    private IEnumerator SlideCoroutine(Vector3 startPos, Vector3 targetPos,  float slideDuration)
    {
        float timeSinceStart = 0f;
        while (timeSinceStart < slideDuration)
        {
            Vector3 newPos = startPos + (timeSinceStart / slideDuration) * (targetPos - startPos);
            _rectTransform.anchoredPosition = newPos;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }
        _rectTransform.anchoredPosition = targetPos;
    }
}
