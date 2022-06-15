using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDrawer : MonoBehaviour
{
    public GameObject borderWallPrefab;
    [Tooltip("If true, place the tiles in a checkboard manner")]
    public bool checkerBoard;
    [Tooltip("Prefab to be instanciated for each cell")]
    public GameObject gridUnitPrefab;
    [Tooltip("Prefab to be instanciated at the border of the walkable area")]
    public GameObject borderRibbonPrefab;
    public float ribbonOffset = 0.3f;
    public GameObject[] decorationPrefabs;
    public float decorationDensity;
    public float minDecorationDistance;
    public float maxDecorationDistance;

}
