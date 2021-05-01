using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMaskUI : MonoBehaviour
{

    public float fadeDuration = 3f;
    private Image _imageComponent;

    // Start is called before the first frame update
    void Start()
    {
        // get image component
        _imageComponent = GetComponent<Image>();
        GameEvents.instance.onGameOver += FadeIn;
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

        while(timeSinceStart < fadeDuration)
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

    void OnDestroy(){
        GameEvents.instance.onGameOver -= FadeIn;
    }



}
