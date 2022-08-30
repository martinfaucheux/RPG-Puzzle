using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CollisionMatrix : SingletoneBase<CollisionMatrix>
{

    public enum Mode
    {
        TOPDOWN,
        ISOMETRIC
    }

    public Vector2Int matrixSize;
    public Vector3 origin;

    public Mode mode = Mode.TOPDOWN;
    public Color sceneBoundsColor;
    public bool showSceneBounds = true;

    [field: Tooltip("If false, all interactions are allowed only if the target cell has a non blocking collider.")]
    [field: SerializeField]
    public bool emptyAllowInteraction { get; private set; } = true;

    // TODO: use GenericGrid instead
    private List<MatrixCollider> _colliderList = new List<MatrixCollider>();

    public void AddCollider(MatrixCollider collider)
    {
        _colliderList.Add(collider);
    }

    public void RemoveCollider(MatrixCollider collider)
    {
        _colliderList.Remove(collider);
    }


    // get the first object found at the given position
    public MatrixCollider GetObjectAtPosition(Vector2Int matrixPosition)
    {
        foreach (MatrixCollider collider in _colliderList)
        {
            if (collider.matrixPosition == matrixPosition)
            {
                return collider;
            }
        }
        return null;
    }

    public List<MatrixCollider> GetObjectsAtPosition(Vector2Int matrixPosition)
    {
        List<MatrixCollider> result = new List<MatrixCollider>();
        foreach (MatrixCollider collider in _colliderList)
        {
            if (collider.matrixPosition == matrixPosition)
            {
                result.Add(collider);
            }
        }
        return result;
    }

    public bool IsValidPosition(Vector2Int matrixPosition)
    {
        int x = matrixPosition.x;
        int y = matrixPosition.y;

        int xMax = matrixSize.x;
        int yMax = matrixSize.y;

        return ((x >= 0) & (y >= 0) & (x < xMax) & (y < yMax));
    }
    public Vector2Int GetMatrixPos(Transform transform) => GetMatrixPos(transform.position);

    public Vector2Int GetMatrixPos(Vector3 realWorldPosition)
    {
        Vector3 realPos = realWorldPosition - origin;
        float x = realPos.x;
        float y = (mode == Mode.TOPDOWN) ? realPos.y : realPos.z;
        return new Vector2Int((int)x, (int)y);
    }

    public void CenterOrigin()
    {
        Vector3 newOrigin = -(Vector2)matrixSize / 2f;
        if (mode == Mode.TOPDOWN)
        {
            origin = newOrigin;
        }
        else
        {
            origin = new Vector3(newOrigin.x, 0, newOrigin.y);
        }
    }

    public Vector3 GetRealWorldPosition(Vector2 matrixPos)
    {
        float x = matrixPos.x;
        float y = matrixPos.y;
        Vector3 realWorldPos;
        if (mode == Mode.TOPDOWN)
            realWorldPos = new Vector3(x, y, 0);
        else
            realWorldPos = new Vector3(x, 0, y);

        return origin + realWorldPos;
    }

    public Vector3 GetRealWorldVector(Direction direction)
    {

        Vector2 pos = direction.ToPos();

        if (mode == Mode.ISOMETRIC)
        {
            return new Vector3(pos.x, 0, pos.y);
        }

        return pos;
    }

}
