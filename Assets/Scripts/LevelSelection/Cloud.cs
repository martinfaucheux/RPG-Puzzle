using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    public Vector3 direction;
    public float speed = 0.5f;
    public float maxDist = 100f;
    public float speedVariation = 0.5f;
    public float rotationVariation = 0.2f;
    public float sizeVariation = 0.2f;
    private Vector3 _direction;
    private float _speed;
    private Transform _childTransform;
    private Quaternion _initRotation;

    void Awake()
    {
        _childTransform = transform.GetChild(0);
        _direction = direction.normalized;
        _initRotation = _childTransform.rotation;
    }

    void OnEnable()
    {
        _speed = speed * (1 + speedVariation * Random.Range(-1f, 1f));
        transform.localScale = (1 + sizeVariation * Random.Range(-1f, 1f)) * Vector3.one;

        _childTransform.rotation = _initRotation;
        _childTransform.RotateAround(
            _childTransform.position,
            _childTransform.forward,
            380f * speedVariation * Random.Range(-1, 1)
        );
    }

    void Update()
    {
        transform.position += _speed * Time.deltaTime * direction;
        if (Vector3.Dot(transform.position, direction) > maxDist)
        {
            gameObject.SetActive(false);
        }
    }
}
