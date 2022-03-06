using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBallSpawner : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] Canvas canvas;

    private Camera _camera;
    private Transform _playerTransform;

    void Start()
    {
        _camera = Camera.main;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        GameEvents.instance.onUnitDies += Spawn;
    }

    void OnDestroy()
    {
        GameEvents.instance.onUnitDies -= Spawn;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Spawn(_playerTransform.position);
    }

    private void Spawn(MonoBehaviour component)
    {
        Spawn(component.transform.position);
    }

    private GameObject Spawn(Vector3 worldPosition)
    {

        Vector2 canvasPosition = GetCanvasPosition(worldPosition);
        Vector3 endPos = targetTransform.position;

        GameObject newGo = GameObjectPool.instance.GetOrCreate("ExpBall");
        ExpBall expBallComponent = newGo.GetComponent<ExpBall>();
        expBallComponent.ResetState();
        newGo.transform.SetParent(canvas.transform);
        ((RectTransform)newGo.transform).anchoredPosition = canvasPosition;

        expBallComponent.Tween(endPos);
        return newGo;
    }

    private Vector3 GetCanvasPosition(Vector3 worldPosition)
    {
        Vector3 screenPos = _camera.WorldToScreenPoint(worldPosition);
        float h = Screen.height;
        float w = Screen.width;
        float x = screenPos.x - (w / 2);
        float y = screenPos.y - (h / 2);
        float s = canvas.scaleFactor;
        return new Vector2(x, y) / s;
    }
}
