﻿using UnityEngine;
using UnityEngine.UI;

public class ExpBall : MonoBehaviour
{
    [SerializeField] float transitionDuration;

    [SerializeField] Image _imageComponent;


    public void Tween(Vector3 endPosition)
    {
        TweenLinear(endPosition);
        TweenAlpha();
        TweenSize();
    }

    public void ResetState()
    {
        Color color = _imageComponent.color;
        color.a = 1;
        _imageComponent.color = color;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
    }

    private void TweenLinear(Vector3 endPosition)
    {
        LeanTween.move(gameObject, endPosition, transitionDuration).setEaseInOutQuint().setOnComplete(OnComplete);
    }

    private void TweenAlpha()
    {
        LeanTween.alpha((RectTransform)transform, 0, transitionDuration).setEaseInQuint();
    }

    private void TweenSize()
    {
        LeanTween.scale(gameObject, 0.25f * new Vector3(1, 1, 0), transitionDuration);
    }

    private void OnComplete()
    {
        gameObject.SetActive(false);
    }

}
