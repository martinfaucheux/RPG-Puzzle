using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinSlide : MonoBehaviour
{
    public float period = 3f;
    public float amplitude = 0.3f;

    private Vector3 _startPos;
    private float _period;
    private float _xRandom;
    private float _yRandom;

    void Start()
    {
        _startPos = transform.position;
        _period = period * Random.Range(0.7f, 0.3f);
        _xRandom = Random.Range(0f, period);
        _yRandom = Random.Range(0f, period);
    }

    void Update()
    {
        Vector3 disp = new Vector3(GetDisplacement(_xRandom), GetDisplacement(_yRandom));
        transform.position = _startPos + disp;
    }

    private float GetDisplacement(float offset)
    {
        return amplitude * Mathf.Sin((offset + 2 * Mathf.PI * Time.time) / _period);
    }
}
