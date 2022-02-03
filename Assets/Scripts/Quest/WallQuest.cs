using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WallQuest", menuName = "Custom Objects / Quest / Wall Quest")]
public class WallQuest : Quest
{
    bool _hasFailed = false;

    MatrixCollider _playerMatrixCollider;
    public override void Initialize()
    {
        base.Initialize();
        _playerMatrixCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<MatrixCollider>();
        GameEvents.instance.onPlayerMove += VisitCell;
    }

    void OnDestroy()
    {
        GameEvents.instance.onPlayerMove -= VisitCell;
    }

    private void VisitCell(Vector2Int matrixPosition)
    {
        if (!_hasFailed)
        {
            if (!IsAdjacentToWall(matrixPosition))
            {
                _hasFailed = true;
            }
        }
    }

    private static bool IsAdjacentToWall(Vector2Int matrixPosition)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (!(x == 0 && y == 0))
                {
                    Vector2Int position = matrixPosition + new Vector2Int(x, y);
                    MatrixCollider adjacentCollider = CollisionMatrix.instance.GetObjectAtPosition(position);
                    if (adjacentCollider != null && adjacentCollider.IsBlocking)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public override bool CheckCompletion()
    {
        return !_hasFailed;
    }
}
