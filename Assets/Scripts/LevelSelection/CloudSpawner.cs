using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{

    public Vector3 p1;
    public Vector3 p2;
    public float period = 2f;
    public string poolKey = "cloud";

    private float _nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time > _nextSpawnTime)
        {
            GameObject obj = GameObjectPool.instance.GetOrCreate(poolKey);
            obj.transform.position = GetStartPos();
            obj.SetActive(true);
            _nextSpawnTime = Time.time + Random.Range(0.5f, 2f) * period;
        }
    }


    private Vector3 GetStartPos() => p1 + Random.Range(0f, 1f) * (p2 - p1);

}
