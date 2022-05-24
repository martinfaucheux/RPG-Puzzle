using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public bool hoverEnabled = false;
    public float hoverAmplitude = 0.1f;
    public float hoverPerdiod = 6f;
    public bool randomize = true;

    private Vector3 _initPos;
    private float _randomness = 0f;

    private void Start()
    {
        _initPos = transform.position;
        if (randomize)
        {
            _randomness = Random.Range(0f, hoverPerdiod);
        }
    }

    private void Update()
    {
        if (hoverEnabled)
        {
            float posDifference = hoverAmplitude * Mathf.Sin(2 * Mathf.PI * (Time.time + _randomness) / hoverPerdiod);
            Vector3 newPos = _initPos + posDifference * Vector3.up;
            transform.position = newPos;
        }
    }
}
