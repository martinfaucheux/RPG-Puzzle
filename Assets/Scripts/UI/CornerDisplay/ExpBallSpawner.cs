using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBallSpawner : SingletoneBase<ExpBallSpawner>
{
    [SerializeField] Transform targetTransform;
    [SerializeField] Canvas canvas;

    private Camera _camera;
    private Transform _playerTransform;

    void Start()
    {
        _camera = Camera.main;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        Spawn(_playerTransform.position);
    }

    public void Spawn(MonoBehaviour component)
    {
        Spawn(component.transform.position);
    }

    public GameObject Spawn(Vector3 worldPosition)
    {

        Vector2 canvasPosition = GetCanvasPosition(worldPosition);
        Vector3 endPos = targetTransform.position;

        GameObject newGo = GameObjectPool.instance.GetOrCreate("ExpBall");
        ExpBall expBallComponent = newGo.GetComponent<ExpBall>();
        newGo.transform.SetParent(canvas.transform);
        expBallComponent.ResetState();
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
