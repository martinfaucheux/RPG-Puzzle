using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMatrix: MonoBehaviour {

    public enum Mode 
    {
        TOPDOWN,
        ISOMETRIC
    }

    //Static instance of GameManager which allows it to be accessed by any other script.
    public static CollisionMatrix instance = null;

    public Vector2Int matrixSize;
    public Vector3 origin;

    public Mode mode = Mode.TOPDOWN;
    public bool showSceneBounds = true;
    public Color sceneBoundsColor;
    public GameObject borderWallPrefab;
    public GameObject GridUnitPrefab;

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


    // get the first object found at the given position
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

    public List<GameObject> GetObjectsAtPosition (Vector2Int matrixPosition)
    {
        List<GameObject> result  = new List<GameObject>();
        foreach (MatrixCollider collider in colliderList){
            if (collider.matrixPosition == matrixPosition)
            {
                result.Add(collider.gameObject);
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

    public Vector2Int GetMatrixPos(Transform transform)
    {
        Vector3 realPos = transform.position - origin;
        float x = realPos.x;
        float y = (mode == Mode.TOPDOWN) ? realPos.y : realPos.z;
        return new Vector2Int((int) x, (int) y);
    }

    public void CenterOrigin()
    {
        Vector3 newOrigin = - (Vector2) matrixSize / 2f;
        if (mode == Mode.TOPDOWN){
            origin = newOrigin;
        }
        else {
            origin = new Vector3(newOrigin.x, 0, newOrigin.y);
        }
    }

    public Vector3 GetRealWorldPosition(Vector2 matrixPos){
        float x = matrixPos.x;
        float y = matrixPos.y;
        Vector3 realWorldPos;
        if (mode == Mode.TOPDOWN)
            realWorldPos = new Vector3(x, y, 0);
        else
            realWorldPos = new Vector3(x, 0, y);
        
        return origin + realWorldPos;
    }

    public Vector3 GetRealWorldVector(Direction direction){
        
        Vector2 pos = direction.ToPos();

        if (mode == Mode.ISOMETRIC){
            return new Vector3(pos.x, 0, pos.y);
        }

        return pos;
    }
    
}
