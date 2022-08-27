using UnityEngine;
using System.Collections;
using System;


// The PrintAwake script is placed on a GameObject.  The Awake function is
// called when the GameObject is started at runtime.  The script is also
// called by the Editor.  An example is when the Scene is changed to a
// different Scene in the Project window.
// The Update() function is called, for example, when the GameObject transform
// position is changed in the Editor.


[ExecuteInEditMode]
public class AutoWallSprite : MonoBehaviour
{

    // TODO: Fix position, they might have been borken when migrating to new Direction class

    public static string wallTag = "Wall";

    public WallSpriteList sprites;

    public bool _hasWallUP = false;
    public bool _hasWallDOWN = false;
    public bool _hasWallLEFT = false;
    public bool _hasWallRIGHT = false;
    public int _countConnectedEdges = 0;

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
    }

    void Update()
    {
        if (FindSpriteRenderer())
        {
            UpdateWallsInfo();

            Sprite sprite = GetAppropriateSprite();
            _spriteRenderer.sprite = sprite;
        }
    }

    private void UpdateWallsInfo()
    {
        _hasWallUP = HasWallAtDirection(Direction.UP);
        _hasWallDOWN = HasWallAtDirection(Direction.DOWN);
        _hasWallLEFT = HasWallAtDirection(Direction.LEFT);
        _hasWallRIGHT = HasWallAtDirection(Direction.RIGHT);

        _countConnectedEdges = 0;
        bool[] boolArray = new bool[] { _hasWallUP, _hasWallDOWN, _hasWallLEFT, _hasWallRIGHT };
        foreach (bool boolDirection in boolArray)
        {
            if (boolDirection)
                _countConnectedEdges++;
        }

    }

    private Sprite GetAppropriateSprite()
    {
        switch (_countConnectedEdges)
        {
            case 0:
                return sprites.aloneSprite;

            case 1:

                if (_hasWallUP)
                    return sprites.topEdgeSprite;
                if (_hasWallDOWN)
                    return sprites.bottomEdgeSprite;
                if (_hasWallLEFT)
                    return sprites.leftEdgeSprite;
                if (_hasWallRIGHT)
                    return sprites.rightEdgeSprite;
                break;

            case 2:

                if (_hasWallUP & _hasWallDOWN)
                    return sprites.horizontalLineSprite;
                if (_hasWallLEFT & _hasWallRIGHT)
                    return sprites.verticalLineSprite;

                if (_hasWallDOWN & _hasWallLEFT)
                    return sprites.bottomLeftSprite;
                if (_hasWallDOWN & _hasWallRIGHT)
                    return sprites.bottomRightSprite;
                if (_hasWallUP & _hasWallLEFT)
                    return sprites.topLeftSprite;
                if (_hasWallUP & _hasWallRIGHT)
                    return sprites.topRightSprite;
                break;

            case 3:

                if (!_hasWallUP)
                    return sprites.bottomTSprite;
                if (!_hasWallDOWN)
                    return sprites.topTSprite;
                if (!_hasWallLEFT)
                    return sprites.rightTSprite;
                if (!_hasWallRIGHT)
                    return sprites.leftTSprite;
                break;

            case 4:
                return sprites.plusSprite;

            default:
                break;

        }

        return null;
    }

    private bool HasWallAtDirection(Direction direction)
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 position = currentPosition + direction.ToPos();
        GameObject wallObj = GetWallAtPosition(position);
        return (wallObj != null);
    }

    private bool HasWallAtAnyDirection()
    {
        foreach (Direction direction in Direction.GetAll<Direction>())
        {
            if (HasWallAtDirection(direction))
            {
                return true;
            }
        }
        return false;
    }

    private GameObject GetWallAtPosition(Vector2 position, float tolerance = 0.01f)
    {
        GameObject[] wallObjList = GameObject.FindGameObjectsWithTag(wallTag);

        foreach (GameObject wallObj in wallObjList)
        {
            float objPosX = wallObj.transform.position.x;
            float objPosY = wallObj.transform.position.y;
            Vector2 objPos = new Vector2(objPosX, objPosY);

            if ((objPos - position).magnitude < tolerance)
            {
                return wallObj;
            }
        }

        return null;
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
}

[Serializable]
public class WallSpriteList
{
    public Sprite aloneSprite;
    public Sprite bottomEdgeSprite;
    public Sprite topEdgeSprite;
    public Sprite leftEdgeSprite;
    public Sprite rightEdgeSprite;
    public Sprite verticalLineSprite;
    public Sprite horizontalLineSprite;
    public Sprite bottomRightSprite;
    public Sprite topRightSprite;
    public Sprite topLeftSprite;
    public Sprite bottomLeftSprite;
    public Sprite bottomTSprite;
    public Sprite topTSprite;
    public Sprite leftTSprite;
    public Sprite rightTSprite;
    public Sprite plusSprite;

}
