using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZigZagQuest", menuName = "Custom Objects / Quest / ZigZag Quest")]
public class ZigZagQuest : Quest
{
    private Vector2Int _lastPosition;
    private Direction _lastDirection = Direction.IDLE;
    private bool hasFailed = false;

    public override void Initialize()
    {
        base.Initialize();
        hasFailed = false;
        _lastDirection = Direction.IDLE;

        _lastPosition = (
            GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<MatrixCollider>()
            .matrixPosition
        );

        GameEvents.instance.onPlayerMove += VisitCell;
    }

    void OnDestroy()
    {
        GameEvents.instance.onPlayerMove -= VisitCell;
    }

    private void VisitCell(Vector2Int cell)
    {
        Vector2Int displacement = cell - _lastPosition;
        Direction direction = Direction.GetFromCoord(displacement);

        if (direction == _lastDirection)
        {
            hasFailed = true;
        }
        _lastPosition = cell;
        _lastDirection = direction;
    }

    public override bool CheckCompletion()
    {
        return !hasFailed;
    }
}