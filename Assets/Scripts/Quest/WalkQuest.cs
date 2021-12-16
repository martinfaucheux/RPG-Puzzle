using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkQuest", menuName = "Custom Objects / Quest / Walk Quest")]
public class WalkQuest : Quest
{
    private GenericGrid<int> visitedCells;

    public override void Initialize()
    {
        base.Initialize();
        InitVisitedCells();

        GameEvents.instance.onPlayerMove += VisitCell;
    }

    void OnDestroy()
    {
        GameEvents.instance.onPlayerMove -= VisitCell;
    }

    private void VisitCell(Vector2Int cell)
    {
        if (visitedCells.ContainsKey(cell))
            visitedCells[cell] += 1;
    }

    public override bool CheckCompletion()
    {
        foreach (KeyValuePair<Vector2Int, int> item in visitedCells)
        {
            if (item.Value != 1)
            {
                return false;
            }
        }
        return true;
    }

    private void InitVisitedCells()
    {
        visitedCells = new GenericGrid<int>();

        Vector2Int matrixSize = CollisionMatrix.instance.matrixSize;
        int w = matrixSize.x;
        int h = matrixSize.y;

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                if (CollisionMatrix.instance.GetObjectAtPosition(position) == null)
                {
                    visitedCells[position] = 0;
                }
            }
        }
    }
}