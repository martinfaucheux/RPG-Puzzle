using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOscillation : MonoBehaviour
{

    [SerializeField] float angle = 10;
    [SerializeField] float period = 5;

    void Start()
    {
        Vector3 rotateVect = new Vector3(0f, 0f, angle);
        LeanTween.rotate(gameObject, rotateVect, period).setFrom(-rotateVect).setPassed(Random.Range(0f, period)).setLoopPingPong();
    }
}
