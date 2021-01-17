using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Direction{

    public enum DirectionValue { UP, DOWN, LEFT, RIGHT, IDLE}

    private DirectionValue value;



    public Direction(DirectionValue value) {
        this.value = value;
    }

    public Direction(int horizontal, int vertical)
    {
        this.value = GetDirectionValueFromCoord(horizontal, vertical);
    }

    public Direction(float horizontal, float vertical)
    {
        this.value = GetDirectionValueFromCoord((int) horizontal, (int) vertical);
    }

    // define conversion methods to new Direction class
    public static implicit operator Direction(Direction2 d) => d.ToPreviousType();
    public static implicit operator Direction2(Direction d){
        switch (d.value){
            case DirectionValue.UP:
                return Direction2.UP;
            case DirectionValue.DOWN:
                return Direction2.DOWN;
            case DirectionValue.LEFT:
                return Direction2.LEFT;
            case DirectionValue.RIGHT:
                return Direction2.RIGHT;
            default:
                return Direction2.IDLE;
        }
    }


    public DirectionValue GetDirectionValueFromCoord(int horizontal, int vertical)
    {
        DirectionValue value;

        if (horizontal != 0) {
            if (horizontal > 0)
                value = DirectionValue.RIGHT;
            else
                value = DirectionValue.LEFT;
        }
        else
        {
            if (vertical > 0)
                value = DirectionValue.UP;
            else if (vertical< 0)
                value = DirectionValue.DOWN;
            else
                value = DirectionValue.IDLE;
        }
        return value;
    }

    public static Direction[] GetAllDirections()
    {
        Direction[] directions = new Direction[]
        {
            new Direction(DirectionValue.UP),
            new Direction(DirectionValue.DOWN),
            new Direction(DirectionValue.LEFT),
            new Direction(DirectionValue.RIGHT)
        };
        return directions;
    }

    public static Direction UP()
    {
        return new Direction(DirectionValue.UP);
    }

    public static Direction DOWN()
    {
        return new Direction(DirectionValue.DOWN);
    }

    public static Direction LEFT()
    {
        return new Direction(DirectionValue.LEFT);
    }

    public static Direction RIGHT()
    {
        return new Direction(DirectionValue.RIGHT);
    }

    public static Direction IDLE()
    {
        return new Direction(DirectionValue.IDLE);
    }

    public Vector2Int ToPos()
    {
        Vector2Int pos = Vector2Int.zero;
        switch (value){
            case (DirectionValue.UP):
                pos = Vector2Int.up;
                break;

            case (DirectionValue.DOWN):
                    pos = Vector2Int.down;
                break;
            case (DirectionValue.LEFT):
                pos = Vector2Int.left;
                break;
            case (DirectionValue.RIGHT):
                pos = Vector2Int.right;
                break;
            case (DirectionValue.IDLE):
                break;
        }
        return pos;
    }

    public bool IsIdle()
    {
        return value == DirectionValue.IDLE;
    }

    public bool IsUp()
    {
        return value == DirectionValue.UP;
    }

    public bool IsDown()
    {
        return value == DirectionValue.DOWN;
    }

    public bool IsRight()
    {
        return value == DirectionValue.RIGHT;
    }

    public bool IsLeft()
    {
        return value == DirectionValue.LEFT;
    }


    public Direction Opposite()
    {
        DirectionValue newValue;
        switch (this.value)
        {
            case DirectionValue.RIGHT:
                newValue = DirectionValue.LEFT;
                break;
            case DirectionValue.LEFT:
                newValue = DirectionValue.RIGHT;
                break;
            case DirectionValue.UP:
                newValue = DirectionValue.DOWN;
                break;
            case DirectionValue.DOWN:
                newValue = DirectionValue.UP;
                break;
            default:
                newValue = DirectionValue.IDLE;
                break;
        }
        return new Direction(newValue);
    }

    public override string ToString()
    {
        string directionString;
        switch (this.value)
        {
            case DirectionValue.RIGHT:
                directionString = "RIGHT";
                break;
            case DirectionValue.LEFT:
                directionString = "LEFT";
                break;
            case DirectionValue.UP:
                directionString = "UP";
                break;
            case DirectionValue.DOWN:
                directionString = "DOWN";
                break;
            default:
                directionString = "IDLE";
                break;
        }
        return directionString;
    }


}