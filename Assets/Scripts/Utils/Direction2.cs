using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Direction2 : Enumeration{

    public static Direction2 IDLE = new Direction2(0, nameof(IDLE), 0, 0);
    public static Direction2 UP = new Direction2(1, nameof(UP), 0, 1);
    public static Direction2 DOWN = new Direction2(2, nameof(DOWN), 0, -1);
    public static Direction2 LEFT = new Direction2(3, nameof(LEFT), -1, 0);
    public static Direction2 RIGHT = new Direction2(4, nameof(RIGHT), 1, 0);

    private int _xValue;
    private int _yValue;

    public Direction2(int id, string name, int xValue, int yValue) : base (id, name){
        this._xValue = xValue;
        this._yValue = yValue;
    }

    public static Direction2 GetDirection2ValueFromCoord(int horizontal, int vertical)
    {
        Direction2 value;

        if (horizontal != 0) {
            if (horizontal > 0)
                value = Direction2.RIGHT;
            else
                value = Direction2.LEFT;
        }
        else
        {
            if (vertical > 0)
                value = Direction2.UP;
            else if (vertical< 0)
                value = Direction2.DOWN;
            else
                value = Direction2.IDLE;
        }
        return value;
    }

    public static Direction2 GetDirection2ValueFromCoord(float horizontal, float vertical){
        return GetDirection2ValueFromCoord((int) horizontal, (int) vertical);
    }

    public Vector2Int ToPos()
    {
        return new Vector2Int(_xValue, _yValue);
    }

    public Direction2 Opposite()
    {
        switch (this.Name)
        {
            case "RIGHT":
                return Direction2.LEFT;
            case "LEFT":
                return Direction2.RIGHT;
            case "UP":
                return Direction2.DOWN;
            case "DOWN":
                return Direction2.UP;
            default:
                return Direction2.IDLE;
        }
    }

    public Direction ToPreviousType(){
        switch (this.Name)
        {
            case "RIGHT":
                return Direction.RIGHT();
            case "LEFT":
                return Direction.LEFT();
            case "UP":
                return Direction.UP();
            case "DOWN":
                return Direction.DOWN();
            default:
                return Direction.IDLE();
        }
    }

}