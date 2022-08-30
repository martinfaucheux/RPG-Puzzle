using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotate : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        transform.RotateAround(transform.position, transform.forward, speed * Time.deltaTime);
    }
}
