using UnityEditor;
using System.Collections;
using UnityEngine;

public class ObjectSetter : EditorWindow
{
    [MenuItem("Tools/Reset Camera angle")]
    static public void MoveSceneViewCamera()
    {
        SceneView.lastActiveSceneView.rotation = Constant.camAngle;
        SceneView.lastActiveSceneView.Repaint();
    }

}