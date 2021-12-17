using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkPattern
{
    w1x1,
    w1x2
}

[CreateAssetMenu(fileName = "WalkPatternQuest", menuName = "Custom Objects / Quest / Walk Pattern Quest")]
public class WalkPatternQuest : Quest
{
    public WalkPattern walkPattern;
    private Vector2Int _lastPosition;
    private Vector2Int _lastTwoPosition;
    private Direction _lastDirection = Direction.IDLE;
    private Direction _lastTwoDirection = Direction.IDLE;
    private bool hasFailed = false;
    private int _walkCounter = 0;

    public override void Initialize()
    {
        base.Initialize();

        hasFailed = false;
        _walkCounter = 0;
        this._lastDirection = Direction.IDLE;
        this._lastTwoDirection = Direction.IDLE;

        _lastPosition = (
            GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<MatrixCollider>()
            .matrixPosition
        );
        _lastTwoPosition = _lastPosition;

        GameEvents.instance.onPlayerMove += VisitCell;
    }

    void OnDestroy()
    {
        GameEvents.instance.onPlayerMove -= VisitCell;
    }

    private void VisitCell(Vector2Int currentPosition)
    {
        _walkCounter++;
        Vector2Int displacement = currentPosition - _lastPosition;
        Direction currentDirection = Direction.GetFromCoord(displacement);

        if (walkPattern == WalkPattern.w1x1)
        {
            hasFailed |= (currentDirection == _lastDirection);
        }
        else if (walkPattern == WalkPattern.w1x2)
        {
            if (_walkCounter % 3 == 0)
            {
                hasFailed |= !(_lastTwoDirection != _lastDirection && _lastDirection == currentDirection);
            }
            else if (_walkCounter % 3 == 1)
            {
                hasFailed |= !(_lastTwoDirection == _lastDirection && _lastDirection != currentDirection);
            }
            else if (_walkCounter % 3 == 2)
            {
                hasFailed |= !(_lastDirection != currentDirection);
            }
        }
        _lastTwoPosition = _lastPosition;
        _lastTwoDirection = _lastDirection;
        _lastPosition = currentPosition;
        _lastDirection = currentDirection;

        if (hasFailed)
            Debug.LogWarning("Quest failed");
    }

    public override bool CheckCompletion()
    {
        return !hasFailed;
    }
}