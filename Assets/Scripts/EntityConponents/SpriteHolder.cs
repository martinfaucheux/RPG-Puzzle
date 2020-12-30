using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolder : MonoBehaviour
{
    public static string spriteHolderName = "SpriteHolder";

    public Transform spriteHolderTransform;
    public bool hoverEnabled = false;
    public float hoverAmplitude = 0.1f;
    public float hoverPerdiod = 6f;

    private SpriteRenderer[] _spriteRenderers;

    private void Awake()
    {
        spriteHolderTransform = transform.Find(spriteHolderName);
    }

    private void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (hoverEnabled)
        {
            float posDifference = hoverAmplitude * Mathf.Sin(2 * Mathf.PI * Time.time / hoverPerdiod);
            Vector3 newSpirteHolderPos = transform.position + posDifference * Vector3.up;
            spriteHolderTransform.position = newSpirteHolderPos;
        }
    }

    public void FaceDirection(Direction direction)
    {
        if (direction.IsRight())
        {
            Vector3 scale = spriteHolderTransform.localScale;
            float scaleX = Mathf.Abs(scale.x);
            scale = new Vector3(scaleX, scale.y, scale.z);
            spriteHolderTransform.localScale = scale;
        }
        else if (direction.IsLeft())
        {
            Vector3 scale = spriteHolderTransform.localScale;
            float scaleX = -Mathf.Abs(scale.x);
            scale = new Vector3(scaleX, scale.y, scale.z);
            spriteHolderTransform.localScale = scale;
        }
    }

    public void BumpOrderLayer(int slide)
    {
        foreach(SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            int newSortingOrder = spriteRenderer.sortingOrder + slide;
            spriteRenderer.sortingOrder = newSortingOrder;
        }
    }

    public void Randomize(bool allowFlipX=true, bool allowFlipY=true, bool allowRotate=true)
    {
        Vector3 scale = spriteHolderTransform.localScale;
        float scaleX = scale.x;
        float scaleY = scale.y;

        if (allowFlipX)
        {
            if (Random.Range(0, 2) == 1)
            {
                scaleX *= -1;
            }
        }
        if (allowFlipY)
        {
            if (Random.Range(0, 2) == 1)
            {
                scaleY *= -1;
            }
        }
        spriteHolderTransform.localScale = new Vector3(scaleX, scaleY, scale.z);

        if (allowRotate)
        {
            int angle_index = (Random.Range(0, 4));
            spriteHolderTransform.Rotate(new Vector3(0f, 0f, angle_index * 90f));
        }
    }

    public void AttackMoveSprite(
        Direction direction,
        float animatonAmplitude = 0.3f,
        float animationDuration = 0.2f
    ){
        StartCoroutine(AttackMoveSpriteCoroutine(direction, animatonAmplitude, animationDuration));
    }

    private IEnumerator AttackMoveSpriteCoroutine(
        Direction direction,
        float amplitude,
        float moveDuration
    ){
        //Vector3 initPos = spriteHolderTransform.position;
        Vector3 initPos = transform.position;
        Vector2 vectDiff = direction.ToPos();
        Vector3 targetPos = spriteHolderTransform.position + (Vector3) vectDiff * amplitude;
        float moveHalfDuration = moveDuration / 2;

        float timeSinceStart = 0f;
        while(timeSinceStart < moveHalfDuration)
        {
            float percentMovement = timeSinceStart / moveHalfDuration;
            Vector3 newPos = initPos + percentMovement * (targetPos - initPos);
            spriteHolderTransform.position = newPos;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        timeSinceStart = 0f;
        while (timeSinceStart < moveHalfDuration)
        {
            float percentMovement = timeSinceStart / moveHalfDuration;
            Vector3 newPos = targetPos + percentMovement * (initPos - targetPos);
            spriteHolderTransform.position = newPos;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        spriteHolderTransform.position = initPos;
    }
}
