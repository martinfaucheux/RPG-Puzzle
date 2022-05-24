using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageNumber : MonoBehaviour
{
    [SerializeField] float popDuration;
    [SerializeField] float fadeDuration;
    [SerializeField] float bumpHeight;

    [SerializeField] TextMeshPro textComponent;

    public void ResetState(string damageText, Color textColor)
    {
        textComponent.color = textColor;
        transform.localScale = Vector3.one;
        textComponent.text = damageText;
    }

    public void Animate()
    {
        StartCoroutine(AnimateCoroutine());
    }

    private IEnumerator AnimateCoroutine()
    {
        // Random start to make it less boring
        yield return new WaitForSeconds(Random.Range(0, 0.2f));

        LeanTween.scale(gameObject, 1.2f * Vector3.one, popDuration).setLoopPingPong(1).setOnComplete(FadeAway);

        // Make a bump animation on the camera y axis
        // TODO: make it a constant
        Vector3 upVect = bumpHeight * (Constant.camAngle * Vector2.up);
        LeanTween.move(gameObject, gameObject.transform.position + upVect, popDuration).setLoopPingPong(1);

        Vector3 targetPos = transform.position + 2 * bumpHeight * (Constant.camAngle * Vector3.up);
        LeanTween.move(gameObject, targetPos, popDuration + fadeDuration);
    }

    private void FadeAway()
    {
        LeanTween.value(textComponent.gameObject, tmProAlphaCallback, 1f, 0f, fadeDuration).setEaseInCubic();
    }

    void tmProAlphaCallback(float alpha)
    {
        Color newColor = textComponent.color;
        newColor.a = alpha;
        textComponent.color = newColor;
    }


    private void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
