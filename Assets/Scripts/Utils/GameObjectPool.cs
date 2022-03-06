using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PoolData
{

    public string poolKey;
    public GameObject prefab;
    public int initCount = 0;

    public PoolData(string poolKey, GameObject prefab, int initCount)
    {
        this.poolKey = poolKey;
        this.prefab = prefab;
        this.initCount = initCount;
    }
}

public class GameObjectPool : MonoBehaviour
{
    public static GameObjectPool instance;
    [SerializeField] List<PoolData> poolData;
    private Dictionary<string, List<GameObject>> _objectPools;

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

    void Start()
    {
        _objectPools = new Dictionary<string, List<GameObject>>();

        foreach (PoolData _poolData in poolData)
        {
            _objectPools[_poolData.poolKey] = new List<GameObject>();
            for (int i = 0; i < _poolData.initCount; i++)
            {
                GameObject newObject = Create(_poolData);
                newObject.SetActive(false);
            }
        }
    }

    public GameObject Create(PoolData poolData)
    {
        GameObject newObject = Instantiate(poolData.prefab, transform.position, Quaternion.identity);
        _objectPools[poolData.poolKey].Add(newObject);
        return newObject;
    }

    public GameObject Create(string poolKey) => Create(GetPoolData(poolKey));

    public GameObject GetOrCreate(PoolData poolData)
    {
        foreach (GameObject pooledObject in _objectPools[poolData.poolKey])
        {
            if (!pooledObject.activeSelf)
            {
                return pooledObject;
            }
        }
        return Create(poolData);
    }

    public GameObject GetOrCreate(string poolKey) => GetOrCreate(GetPoolData(poolKey));

    public PoolData GetPoolData(string poolKey)
    {
        foreach (PoolData _poolData in poolData)
        {
            if (_poolData.poolKey == poolKey)
            {
                return _poolData;
            }
        }
        return null;
    }
}
