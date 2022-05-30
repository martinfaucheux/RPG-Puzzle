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
        GameEvents.instance.onEndOfLevel += Trigger;
    }

    void OnDestroy()
    {
        GameEvents.instance.onEndOfLevel -= Trigger;
    }

    private void Trigger()
    {
        StartCoroutine(TriggerCouroutine());
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
