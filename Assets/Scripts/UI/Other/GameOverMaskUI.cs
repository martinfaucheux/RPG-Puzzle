using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: to be DEPRECATED

public class GameOverMaskUI : MonoBehaviour
{

    public float fadeDuration = 3f;
    private Image _imageComponent;

    // Start is called before the first frame update
    void Start()
    {
        // get image component
        _imageComponent = GetComponent<Image>();
        GameEvents.instance.onEnterState += OnEnterState;
    }

    void OnDestroy()
    {
        GameEvents.instance.onEnterState -= OnEnterState;
    }

    private void OnEnterState(GameState state)
    {
        if (state == GameState.GAME_OVER)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        _imageComponent.enabled = true;
        float timeSinceStart = 0f;
        Color spriteColor = _imageComponent.color;

        while (timeSinceStart < fadeDuration)
        {
            float alphaValue = timeSinceStart / fadeDuration;
            spriteColor.a = alphaValue;
            _imageComponent.color = spriteColor;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        spriteColor.a = 1;
        _imageComponent.color = spriteColor;
    }
}
