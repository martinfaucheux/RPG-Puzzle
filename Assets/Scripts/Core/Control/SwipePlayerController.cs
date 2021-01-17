using System;
using UnityEngine;

public class SwipePlayerController : PlayerController
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            Direction direction = GetDirection(fingerDownPosition - fingerUpPosition);
            TriggerMovement(direction);
        }
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private Direction GetDirection(Vector2 swipeVector){
        float x = swipeVector.x;
        float y = swipeVector.y;
        if (Mathf.Abs(x) > Mathf.Abs(y)){
            if (x > 0f)
                return Direction.RIGHT;
            else
                return Direction.LEFT;
        }
        else{
           if (y > 0f)
                return Direction.UP;
            else
                return Direction.DOWN;
        }
    }
}

