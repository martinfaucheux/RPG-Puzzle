using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class AutoRandomSpriteHolder: MonoBehaviour
{
    //public Sprite[] spriteList;

    public bool allowFlipX = false;
    public bool allowFlipY = false;
    public bool allowRotate = false;

    private SpriteHolder _spriteHolder;

    void Awake()
    {
        if (FindSpriteRenderer())
        {
            // _spriteRenderer.sprite = PickRandomSprite();
            _spriteHolder.Randomize(allowFlipX, allowFlipY, allowRotate);
        }
    }

    //private Sprite PickRandomSprite()
    //{
    //    int spriteCount = 0;
    //    if (spriteList != null)
    //        spriteCount = spriteList.Length;

    //    if (spriteCount == 0)
    //        return null;

    //    // Create a Random object  
    //    Random rand = new Random();

    //    // Generate a random index less than the size of the array.  
    //    int index = Random.Range(0, spriteCount);

    //    return spriteList[index];
    //}

    private bool FindSpriteRenderer()
    {
        if (_spriteHolder != null)
        {
            return true;
        }

        _spriteHolder = GetComponent<SpriteHolder>();

        return (_spriteHolder != null);
    }
}
