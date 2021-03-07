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
    private Transform _playerTransform;
    private Vector3 _defaultPosition;

    // Start is called before the first frame update
    void Start()
    {
        _defaultPosition = transform.position;
        // _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        _playerCollider = playerGO.GetComponent<MatrixCollider>();
        _playerTransform = playerGO.GetComponent<Transform>();

        // listen to the event
        GameEvents.instance.onGameOver += SlowTargetPlayer;
    }

    private void TargetPlayer()
    {
        Target(_playerTransform, transitionTime);
    }

    private void SlowTargetPlayer()
    {
        Target(_playerTransform, slowTransitionTime);
    }

    private void TargetPosition(Vector3 position, float transitionTime)
    {
        StartCoroutine(SmoothTarget(position, transitionTime));
    }

    private void Target(Transform targetTransform, float transitionTime)
    {
        StartCoroutine(SmoothTarget(targetTransform, transitionTime));
    }

    private void Reset()
    {
        StartCoroutine(SmoothTarget(_defaultPosition, transitionTime));
    }

    private IEnumerator SmoothTarget(Vector3 targetPos, float transitionTime)
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

    private IEnumerator SmoothTarget(Transform targetTransform, float transitionTime)
    {
        Vector3 initPos = transform.position;
        Vector3 targetPos3d;
        Vector3 newPos;
        
        float timeSinceStart = 0f;

        isMoving = true;
        while (timeSinceStart < transitionTime)
        {
            targetPos3d = targetTransform.position;
            Debug.Log("target pos " + targetPos3d.ToString());

            
            newPos = initPos + (targetPos3d - initPos) * (timeSinceStart / transitionTime);
            transform.position = newPos;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
        transform.position = targetTransform.position;
    }


}
