using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShakeDetector : MonoBehaviour
{

    [SerializeField] float shakeDetectionThreshold = 2f;
    [SerializeField] float minShakeTimeInterval = 0.7f;
    [SerializeField] UnityEvent onShakeEvent;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;


    void Start()
    {
        // We want to compare squqre values 
        sqrShakeDetectionThreshold = Mathf.Pow(shakeDetectionThreshold, 2);
    }


    void Update()
    {
        if (
            Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
            && Time.unscaledTime >= timeSinceLastShake + minShakeTimeInterval
        ){
            onShakeEvent.Invoke();
            timeSinceLastShake = Time.unscaledTime;
        }
    }
}
