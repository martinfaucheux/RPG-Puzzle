using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InstructionPanel : SingletoneBase<InstructionPanel>
{
    [SerializeField] float showDelay = 0.5f;
    [SerializeField] float scaleFactor = 1.1f;
    [SerializeField] float animationDuration = 0.1f;
    [SerializeField] Image scalingImageComponent;
    [SerializeField] float _fadeDuration = 0.2f;
    [SerializeField] TextMeshProUGUI _textComponent;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] CanvasGroup _pressKeyCanvasGroup;
    private bool isShowing = false;
    void Start()
    {
        PlayInputManager.instance.playerInputActions.UI.AnyKey.performed += Hide;
    }

    public void Show(string text)
    {
        _textComponent.text = text;
        StartCoroutine(ShowClickMessage());
    }

    public void Hide()
    {
        if (isShowing)
        {
            Fade(0f);
            PlayInputManager.instance.SwitchCurrentActionMap("Player");
            StateManager.instance.SetState(GameState.PLAY);
            isShowing = false;
        }
    }

    private void Hide(InputAction.CallbackContext context) => Hide();


    private IEnumerator ShowClickMessage()
    {
        yield return new WaitForSecondsRealtime(showDelay);
        StateManager.instance.SetState(GameState.INSTRUCTION);
        PlayInputManager.instance.SwitchCurrentActionMap("UI");
        Fade(1);
        BumpAnimation();
        yield return new WaitForSecondsRealtime(showDelay * 2);
        LeanTween.value(
            gameObject,
            v => _pressKeyCanvasGroup.alpha = v,
            0f,
            1f,
            1f
        ).setLoopPingPong();
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

    private LTDescr Fade(float targetValue)
    {
        return LeanTween.value(
            gameObject,
            v => _canvasGroup.alpha = v,
            _canvasGroup.alpha,
            targetValue,
            _fadeDuration
        );
    }
}
