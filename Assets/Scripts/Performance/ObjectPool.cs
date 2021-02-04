using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static Dictionary<string, ObjectPool> _poolDict;
    public GameObject prefab;
    public string poolName;
    public int defaultPoolSize;
    private int _poolSize;

    private List<GameObject> _objectList;

    void Awake()
    {
        if(_poolDict == null){
            _poolDict = new Dictionary<string, ObjectPool>();
        }

        //Check if instance already exists
        if (!_poolDict.ContainsKey(poolName)){
            //if not, set instance to this
            _poolDict.Add(poolName, this);
        }
        
        else if (_poolDict[poolName] == null){
            _poolDict.Add(poolName, this);
        }


        //If instance already exists and it's not this:
        else if (_poolDict[poolName] != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a CollisionMatrix.
            Destroy(gameObject);

        // foreach(KeyValuePair<string, ObjectPool> kvp in _poolDict){
        //     string key = (string) kvp.Key;
        //     ObjectPool val = (ObjectPool) kvp.Value;
        //     Debug.Log("a");
        //     Debug.Log(val);
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        _objectList = new List<GameObject>();
        for(int i= 0; i < defaultPoolSize; i++){
            InstantiateNewObject();
        }   
    }

    public static ObjectPool GetPool(string poolName){
        try{
            return _poolDict[poolName];
        }
        catch(KeyNotFoundException){
            throw new System.Exception("This pool doesn't exist: " + poolName);
        }
    }

    public GameObject GetPooledObject(){
        foreach(GameObject gameObject in _objectList){
            if(!gameObject.activeInHierarchy){
                return gameObject;
            }
        }
        return InstantiateNewObject();
    }

    private GameObject InstantiateNewObject(){
        GameObject gameObject = Instantiate(prefab);
        gameObject.SetActive(false);
        _objectList.Add(gameObject);
        return gameObject;
    }
}
