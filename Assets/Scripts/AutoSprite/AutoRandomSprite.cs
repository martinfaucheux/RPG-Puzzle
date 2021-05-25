using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class AutoRandomSprite : MonoBehaviour
{
    public Sprite[] spriteList;
    public bool allowRotation = false;
    public bool allowVerticalFlip = false;
    public bool allowHorizontalFlip = false;

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        if (FindSpriteRenderer())
        {
            _spriteRenderer.sprite = PickRandomSprite();
            TweakImage();
        }
    }

    private Sprite PickRandomSprite()
    {
        int spriteCount = 0;
        if (spriteList != null)
            spriteCount = spriteList.Length;

        if (spriteCount == 0)
            return null;

        // Generate a random index less than the size of the array.  
        int index = Random.Range(0, spriteCount);

        return spriteList[index];
    }

    private bool FindSpriteRenderer()
    {
        if (_spriteRenderer != null)
        {
            return true;
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();

        return (_spriteRenderer != null);
    }

    private void TweakImage()
    {
        if (allowHorizontalFlip)
        {
            if (Random.Range(0, 2) == 1)
            {
                _spriteRenderer.flipX = true;
            }
        }

        if (allowVerticalFlip)
        {
            if (Random.Range(0, 2) == 1)
            {
                _spriteRenderer.flipY = true;
            }
        }

        if (allowRotation)
        {
            int angle_index = (Random.Range(0, 4));
            transform.Rotate(new Vector3(0f, 0f, angle_index * 90f));
        }
    }
}
