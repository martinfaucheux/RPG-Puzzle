using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolder : MonoBehaviour
{
    public static string spriteHolderName = "SpriteHolder";
    private static string _spriteGOMarker = "sgo";

    public Transform spriteHolderTransform;
    public bool flipForFacing = false;

    private bool _hasUniqueSprite = true;
    public Animator activeAnimator;
    private GameObject _sgoNW;
    private GameObject _sgoNE;
    private GameObject _sgoSE;
    private GameObject _sgoSW;

    private SpriteRenderer[] _spriteRenderers;

    private void Awake()
    {
        spriteHolderTransform = transform.Find(spriteHolderName);
    }

    private void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        DiscoverSpriteGameObjects();
        activeAnimator = GetComponentInChildren<Animator>(false);
    }

    public void FaceDirection(Direction direction)
    {
        if (direction == Direction.IDLE)
            return;

        if (_hasUniqueSprite & flipForFacing)
        {
            FlipSprite(direction);
        }
        else if (!_hasUniqueSprite)
        {
            foreach (GameObject go in GetSpriteGameObjects())
            {
                if (go != null)
                {
                    go.SetActive(false);
                }
            }
            GameObject spriteGO = GetSpriteGOForDirection(direction);
            if (spriteGO != null)
            {
                spriteGO.SetActive(true);
                Animator childAnimator = spriteGO.GetComponent<Animator>();
                if (childAnimator != null)
                {
                    activeAnimator = childAnimator;
                }
            }
        }
    }

    private void FlipSprite(Direction direction)
    {
        if (direction == Direction.RIGHT)
        {
            Vector3 scale = spriteHolderTransform.localScale;
            float scaleX = Mathf.Abs(scale.x);
            scale = new Vector3(scaleX, scale.y, scale.z);
            spriteHolderTransform.localScale = scale;
        }
        if (direction == Direction.LEFT)
        {
            Vector3 scale = spriteHolderTransform.localScale;
            float scaleX = -Mathf.Abs(scale.x);
            scale = new Vector3(scaleX, scale.y, scale.z);
            spriteHolderTransform.localScale = scale;
        }
    }

    public void BumpOrderLayer(int slide)
    // TODO: this should go to another component. Used only in GameOverMask FadeIn method
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            int newSortingOrder = spriteRenderer.sortingOrder + slide;
            spriteRenderer.sortingOrder = newSortingOrder;
        }
    }

    public void Randomize(bool allowFlipX = true, bool allowFlipY = true, bool allowRotate = true)
    // TODO: this should go to another component
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
    )
    {
        StartCoroutine(AttackMoveSpriteCoroutine(direction, animatonAmplitude, animationDuration));
    }

    private IEnumerator AttackMoveSpriteCoroutine(
        Direction direction,
        float amplitude,
        float moveDuration
    )
    {
        //Vector3 initPos = spriteHolderTransform.position;
        Vector3 initPos = transform.position;
        Vector3 vectDiff = CollisionMatrix.instance.GetRealWorldVector(direction);
        Vector3 targetPos = spriteHolderTransform.position + vectDiff * amplitude;
        float moveHalfDuration = moveDuration / 2;

        float timeSinceStart = 0f;
        while (timeSinceStart < moveHalfDuration)
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

    private void DiscoverSpriteGameObjects()
    {
        foreach (Transform childTransform in spriteHolderTransform)
        {
            if (childTransform.name.StartsWith(_spriteGOMarker))
            {
                string transformName = childTransform.name.Replace(_spriteGOMarker, "");

                switch (transformName)
                {
                    case "NW":
                        _sgoNW = childTransform.gameObject;
                        break;
                    case "NE":
                        _sgoNE = childTransform.gameObject;
                        break;
                    case "SE":
                        _sgoSE = childTransform.gameObject;
                        break;
                    case "SW":
                        _sgoSW = childTransform.gameObject;
                        break;
                    default:
                        break;
                }
            }
        }
        if (
            _sgoNW != null
            & _sgoNE != null
            & _sgoSW != null
            & _sgoSE != null
        )
        {
            _hasUniqueSprite = false;
        }
    }

    private GameObject[] GetSpriteGameObjects()
    {
        return new GameObject[]{
            _sgoNW, _sgoNE, _sgoSW, _sgoSE
        };
    }

    private GameObject GetSpriteGOForDirection(Direction direction)
    {
        switch (direction.SpriteDirection)
        {
            case "NW":
                return _sgoNW;
            case "NE":
                return _sgoNE;
            case "SE":
                return _sgoSE;
            case "SW":
                return _sgoSW;
            default:
                return null;
        }
    }

    public void HideSprites()
    {
        foreach (GameObject spriteGameObjects in GetSpriteGameObjects())
        {
            spriteGameObjects.SetActive(false);
        }
    }
}
