using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Direction : Enumeration{

    public static Direction IDLE = new Direction(0, nameof(IDLE), 0, 0, "");
    public static Direction UP = new Direction(1, nameof(UP), 0, 1, "NW");
    public static Direction DOWN = new Direction(2, nameof(DOWN), 0, -1, "SE");
    public static Direction LEFT = new Direction(3, nameof(LEFT), -1, 0, "SW");
    public static Direction RIGHT = new Direction(4, nameof(RIGHT), 1, 0, "NE");

    public string SpriteDirection{get { return _isoSpriteDirection; }}
    private int _xValue;
    private int _yValue;
    private string _isoSpriteDirection;

    public Direction(int id, string name, int xValue, int yValue, string isoSpriteDirection) : base (id, name){
        this._xValue = xValue;
        this._yValue = yValue;
        this._isoSpriteDirection = isoSpriteDirection;
    }

    public static Direction GetDirection2ValueFromCoord(int horizontal, int vertical)
    {
        Direction value;

        if (horizontal != 0) {
            if (horizontal > 0)
                value = Direction.RIGHT;
            else
                value = Direction.LEFT;
        }
        else
        {
            if (vertical > 0)
                value = Direction.UP;
            else if (vertical< 0)
                value = Direction.DOWN;
            else
                value = Direction.IDLE;
        }
        return value;
    }

    public static Direction GetDirection2ValueFromCoord(float horizontal, float vertical){
        return GetDirection2ValueFromCoord((int) horizontal, (int) vertical);
    }

    public static Direction GetDirection2ValueFromCoord(Vector2 coord){
        return GetDirection2ValueFromCoord((int) coord.x, (int) coord.y);
    }

    public Vector2Int ToPos()
    {
        return new Vector2Int(_xValue, _yValue);
    }

    public Direction Opposite()
    {
        switch (this.Name)
        {
            case "RIGHT":
                return Direction.LEFT;
            case "LEFT":
                return Direction.RIGHT;
            case "UP":
                return Direction.DOWN;
            case "DOWN":
                return Direction.UP;
            default:
                return Direction.IDLE;
        }
    }
}