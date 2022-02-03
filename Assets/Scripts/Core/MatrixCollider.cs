using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixCollider : MonoBehaviour
{

    public Vector2Int matrixPosition;
    public bool IsBlocking = false;

    private CollisionMatrix _collisionMatrix;

    // Use this for initialization
    void Start()
    {
        _collisionMatrix = CollisionMatrix.instance;
        matrixPosition = _collisionMatrix.GetMatrixPos(this.transform);
        _collisionMatrix.AddCollider(this);
    }

    private void OnDestroy()
    {
        _collisionMatrix?.RemoveCollider(this);
    }

    public Vector3 GetRealPos()
    {
        return _collisionMatrix.GetRealWorldPosition(matrixPosition);
    }

    public bool IsValidDirection(Direction direction)
    {
        Vector2Int futureMatrixPosition = matrixPosition + direction.ToPos();
        return _collisionMatrix.IsValidPosition(futureMatrixPosition);
    }

    public MatrixCollider GetObjectInDirection(Direction direction)
    {
        Vector2Int positionToCheck = matrixPosition + direction.ToPos();

        if (!_collisionMatrix.IsValidPosition(positionToCheck))
        {
            return null;
        }

        return _collisionMatrix.GetObjectAtPosition(positionToCheck);
    }


    public Direction GetDirectionToOtherCollider(MatrixCollider otherCollider)
    {
        Vector2Int posDiff = otherCollider.matrixPosition - this.matrixPosition;
        return Direction.GetFromCoord(posDiff.x, posDiff.y);
    }


}
