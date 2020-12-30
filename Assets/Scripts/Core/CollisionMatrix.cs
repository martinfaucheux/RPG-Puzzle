using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMatrix: MonoBehaviour { 

    //Static instance of GameManager which allows it to be accessed by any other script.
    public static CollisionMatrix instance = null;

    public Vector2Int matrixSize;
    public Vector2 origin;

    public bool showSceneBounds = true;
    public Color sceneBoundsColor;
    public GameObject borderWallPrefab;

    private List<MatrixCollider> colliderList = new List<MatrixCollider>();

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a CollisionMatrix.
            Destroy(gameObject);

    }

    public void AddCollider(MatrixCollider collider)
    {
        colliderList.Add(collider);
    }

    public void RemoveCollider(MatrixCollider collider)
    {
        colliderList.Remove(collider);
    }


    public GameObject GetObjectAtPosition (Vector2Int matrixPosition)
    {
        foreach (MatrixCollider collider in colliderList){
            if (collider.matrixPosition == matrixPosition)
            {
                return collider.gameObject;
            }
        }

        return null;
    }

    public bool IsValidPosition(Vector2Int matrixPosition)
    {
        int x = matrixPosition.x;
        int y = matrixPosition.y;

        int xMax = matrixSize.x;
        int yMax = matrixSize.y;

        return ((x >= 0) & (y >= 0) & (x < xMax) & (y < yMax));
    }

    public Vector2Int GetMatrixPos(Transform transform)
    {
        Vector2 realPos = new Vector2(transform.position.x, transform.position.y);
        realPos -= origin;
        Vector2Int result = new Vector2Int((int)realPos.x, (int)realPos.y);
        return result;
    }

    public void CenterOrigin()
    {
        origin = - (Vector2) matrixSize / 2f;
    }
}
