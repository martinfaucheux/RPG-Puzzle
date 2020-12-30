using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTranslation : MonoBehaviour
{
    public float transitionTime = 1f;
    public float slowTransitionTime = 3f;
    public bool isMoving = false;

    // private Transform _playerTransform;
    private MatrixCollider _playerCollider;
    private Vector3 _defaultPosition;

    // Start is called before the first frame update
    void Start()
    {
        _defaultPosition = transform.position;
        // _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<MatrixCollider>();

        // listen to the event
        GameEvents.instance.onEnterLevelUp += TargetPlayer;
        GameEvents.instance.onEndOfLevel += TargetPlayer;
        GameEvents.instance.onGameOver += SlowTargetPlayer;

        GameEvents.instance.onExitLevelUp += Reset;
    }

    private void TargetPlayer()
    {
        TargetPosition(_playerCollider, transitionTime);
    }

    private void SlowTargetPlayer()
    {
        TargetPosition(_playerCollider, slowTransitionTime);
    }

    private void TargetPosition(Vector3 position, float transitionTime)
    {
        StartCoroutine(SmoothTargetPosition(position, transitionTime));
    }

    private void TargetPosition(MatrixCollider collider, float transitionTime)
    {
        StartCoroutine(SmoothTargetCollider(collider, transitionTime));
    }

    private void Reset()
    {
        StartCoroutine(SmoothTargetPosition(_defaultPosition, transitionTime));
    }

    private IEnumerator SmoothTargetCollider(MatrixCollider collider, float transitionTime)
    {
        Vector3 targetPos3d = collider.GetRealPos();
        targetPos3d.z = _defaultPosition.z;

        Vector3 initPos = transform.position;

        float timeSinceStart = 0f;

        isMoving = true;
        while (timeSinceStart < transitionTime)
        {
            targetPos3d = collider.GetRealPos();
            targetPos3d.z = _defaultPosition.z;

            Vector3 newPos = initPos + (targetPos3d - initPos) * (timeSinceStart / transitionTime);
            transform.position = newPos;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
        transform.position = targetPos3d;
    }

    private IEnumerator SmoothTargetPosition(Vector3 targetPos, float transitionTime)
    {
        Vector3 targetPos3d = new Vector3(targetPos.x, targetPos.y, _defaultPosition.z);
        Vector3 initPos = transform.position;

        float timeSinceStart = 0f;

        isMoving = true;
        while (timeSinceStart < transitionTime)
        {
            Vector3 newPos = initPos + (targetPos3d - initPos) * (timeSinceStart / transitionTime);
            transform.position = newPos;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
        transform.position = targetPos3d;
    }
}
