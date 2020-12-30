using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugUtils
{
    public static void DrawCross(Vector3 position, float size = 1f){
        Color color = Color.red;
        Vector3 vertical = new Vector3(size / 2f, 0f, 0f);
        Vector3 horizontal = new Vector3(0f, size / 2f, 0f);

        Debug.DrawLine(position - vertical, position + vertical, color, 3600);
        Debug.DrawLine(position - horizontal, position + horizontal, color, 3600);
    }
}
