using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpAnimHandler : MonoBehaviour
{
    [SerializeField] SpriteFlasher spriteFlasher;
    [SerializeField] Animator animator;
    [SerializeField] TextMeshPro floatingTextComponent;
    [SerializeField] float textAnimationDuration = 1.5f;

    private Vector3 initFloatingTextPos;

    void Start()
    {
        GameEvents.instance.onLevelUp += AnimateLevelUp;
        initFloatingTextPos = floatingTextComponent.transform.localPosition;
    }

    public void AnimateLevelUp()
    {
        spriteFlasher.Flash();
        animator.SetTrigger("levelup");

        AnimateFloatingText();
    }

    private void AnimateFloatingText()
    {
        Transform t = floatingTextComponent.transform;
        t.localPosition = initFloatingTextPos;
        Vector3 targetPos = t.position + 2f * Vector3.up;

        floatingTextComponent.gameObject.SetActive(true);
        LeanTween.move(t.gameObject, targetPos, textAnimationDuration).setOnComplete(DisableFloatingText);

        floatingTextComponent.alpha = 1f;
        LeanTween.value(floatingTextComponent.gameObject, tmProAlphaCallback, 1f, 0f, textAnimationDuration).setEaseInCubic();
    }


    void tmProAlphaCallback(float alpha)
    {
        Color newColor = floatingTextComponent.color;
        newColor.a = alpha;
        floatingTextComponent.color = newColor;
    }




    private void DisableFloatingText()
    {
        floatingTextComponent.gameObject.SetActive(false);
    }
}
