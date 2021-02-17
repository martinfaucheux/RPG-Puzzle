using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{

    public bool hoverEnabled = false;

    public float hoverAmplitude = 0.1f;
    public float hoverPerdiod = 6f; 

    private Vector3 _initPos;
    private void Start(){
        _initPos = transform.position;
    }

    private void Update()
    {
        if (hoverEnabled)
        {
            float posDifference = hoverAmplitude * Mathf.Sin(2 * Mathf.PI * Time.time / hoverPerdiod);
            Vector3 newPos = _initPos + posDifference * Vector3.up;
            transform.position = newPos;
        }
    }
}
