using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMask : MonoBehaviour
{

    public float fadeDuration = 3f;
    public int playerLayerBumpOrder = 199;

    private SpriteHolder _playerSpriteHolder;
    private SpriteRenderer _maskSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // get sprite renderers
        _playerSpriteHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteHolder>();
        _maskSpriteRenderer = GetComponent<SpriteRenderer>();

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
        _playerSpriteHolder.BumpOrderLayer(playerLayerBumpOrder);
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float timeSinceStart = 0f;
        Color spriteColor = _maskSpriteRenderer.color;

        while (timeSinceStart < fadeDuration)
        {
            float alphaValue = timeSinceStart / fadeDuration;
            spriteColor.a = alphaValue;
            _maskSpriteRenderer.color = spriteColor;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        spriteColor.a = 1;
        _maskSpriteRenderer.color = spriteColor;
    }
}
