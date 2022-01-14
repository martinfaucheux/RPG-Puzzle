using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionScroll : MonoBehaviour
{
    [SerializeField] private float showDelay = 0.5f;
    [SerializeField] private bool showOnLevelLoaded;
    [SerializeField] private float scaleFactor = 1.1f;
    [SerializeField] private float animationDuration = 0.1f;
    [SerializeField] private Image scalingImageComponent;
    private bool isShowing = false;
    void Start()
    {
        if (showOnLevelLoaded)
        {
            DelayShow();
        }
        else
        {
            Hide();
        }
    }

    public void DelayShow()
    {
        StartCoroutine(ShowCoroutine());
    }

    public void Hide()
    {
        if (isShowing)
        {
            foreach (Transform childTransform in transform)
            {
                childTransform.gameObject.SetActive(false);
            }
            GameManager.instance.isInstruction = false;
            isShowing = false;
        }
    }

    private IEnumerator ShowCoroutine()
    {
        GameManager.instance.isInstruction = true;
        yield return new WaitForSecondsRealtime(showDelay);
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(true);
        }
        BumpAnimation();
    }



    private void BumpAnimation()
    {
        Vector3 targetScale = scaleFactor * Vector3.one;

        scalingImageComponent.transform.localScale = Vector3.zero;
        LeanTween.scale(scalingImageComponent.gameObject, targetScale, animationDuration).setEaseOutCubic().setOnComplete(BumpAnimationAdjust);
    }

    private void BumpAnimationAdjust()
    {
        LeanTween.scale(scalingImageComponent.gameObject, Vector3.one, animationDuration * 0.25f).setOnComplete(BumpAnimationAdjust);
        isShowing = true;
    }
}
