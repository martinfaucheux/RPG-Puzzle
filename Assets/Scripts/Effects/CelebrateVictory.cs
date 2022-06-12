using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrateVictory : MonoBehaviour
{
    [SerializeField] SpriteRenderer knightSpriteRender;
    [SerializeField] SpriteRenderer diamondSpriteRender;
    [SerializeField] SpriteHolder spriteHolder;

    void Start()
    {
        GameEvents.instance.onEnterState += OnEnterState;
    }

    void OnDestroy()
    {
        GameEvents.instance.onEnterState -= OnEnterState;
    }

    private void OnEnterState(GameState state)
    {
        if (state == GameState.WIN)
        {
            StartCoroutine(TriggerCouroutine());
        }
    }

    private IEnumerator TriggerCouroutine()
    {
        // wait for movement to finish
        yield return new WaitForSeconds(GameManager.instance.actionDuration);

        spriteHolder.HideSprites();
        knightSpriteRender.enabled = true;
        diamondSpriteRender.enabled = true;
    }
}
