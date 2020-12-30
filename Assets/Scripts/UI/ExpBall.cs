using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : MonoBehaviour
{

    private Vector3 _targetPos = Vector3.zero;
    // public float speed = 1f;

    private Vector3 startPoint;
    private Vector3 endPoint;

    [Range(0, 1)] //Range between 0 and 1 that evolves from 0 to 1 during the animation
    public float smoothness;

    //Scale of the object before starting to lerp it
    private Vector3 ownScale;
    // void Start()
    // {
    //     ownScale = transform.localScale; //Save the scale before changing


    // _targetPos = ExpDisplay.instance.transform.position;

    // // Find the points to start and to target, will need to put it in Update()
    // // if the player is moving during the animation 
    // startPoint = transform.position;
    // endPoint = Camera.main.ScreenToWorldPoint(new Vector3(_targetPos.x, _targetPos.y, 1));
    // }
 
    // void Update()
    // {
    //     transform.position = Vector3.Lerp(startPoint, endPoint, smoothness);
    //     transform.localScale = ownScale * (1 - smoothness);
    // }







    // void Start()
    // {
    //     _targetPos = ExpDisplay.instance.transform.position;
    // }

    // void Update()
    // {
    //     Vector3 difference = (transform.position - _targetPos); 
    //     float distance = difference.magnitude;

    //     if( distance < 0.1f){
    //         Destroy(this);
    //         return;
    //     }

    //     float magnitude = Mathf.Min(distance, speed * Time.deltaTime);

    //     Vector3 newPostion = magnitude * difference.normalized;
        
    //     transform.position = newPostion;
    // }
}
