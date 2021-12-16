using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericGrid<T> : IEnumerable
{

    private Dictionary<Vector2Int, T> _dict = new Dictionary<Vector2Int, T>();

    public T this[Vector2Int vect]
    {
        get => _dict[vect];
        set => _dict[vect] = value;
    }

    public T this[int x, int y]
    {
        get => this[new Vector2Int(x, y)];
        set => _dict[new Vector2Int(x, y)] = value;
    }

    public bool ContainsKey(Vector2Int vect) => _dict.ContainsKey(vect);

    public List<T> GetNeighbors(Vector2Int vect)
    {
        List<T> result = new List<T>();

        Vector2Int[] offsets = new Vector2Int[]{
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1)
        };

        foreach (Vector2Int offset in offsets)
        {
            Vector2Int gridPos = vect + offset;
            if (ContainsKey(gridPos))
            {
                result.Add(this[gridPos]);
            }
        }
        return result;
    }

    public IEnumerator GetEnumerator()
    {
        return _dict.GetEnumerator();
    }

}
