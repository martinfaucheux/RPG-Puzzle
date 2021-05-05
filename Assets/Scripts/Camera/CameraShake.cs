using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    public float defaultShakeDuration = 0.5f;
    public float defaultShakeMagnitude = 0.5f;
    public float defaultShakeAngle = 1f;

    public int defaultShakeOccurences = 1;

    private bool _isShaking = false;
    private Vector3 _initPos;
    private Quaternion _initRotation;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a CollisionMatrix.
            Destroy(gameObject);
    }

    private void Start()
    {
        _initPos = transform.position;
        _initRotation = transform.rotation;
        GameEvents.instance.onPlayerGetDamage += Shake;
    }

    private void Update()
    {
        // TODO: remove
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shake();
        }
    }

    public void Shake()
    {
        Shake(defaultShakeDuration, defaultShakeMagnitude, defaultShakeOccurences);
    }

    public void Shake(float duration, float magnitude, int occurences)
    {
        if (!_isShaking)
        {
            StartCoroutine(ShakeCoroutine(duration, magnitude, occurences));
        
            if (defaultShakeAngle > 0f)
            {
                StartCoroutine(ShakeRotationCoroutine(duration, defaultShakeAngle, occurences));
            }
        }
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude, int occurences)
    {
        _isShaking = true;

        for(int i=0; i < occurences; i++){

            Vector3 unitVector = GetRandomUnitVector(magnitude);
            float timeSinceStart = 0f;
            while(timeSinceStart < duration)
            {
                float multiplier = (duration - timeSinceStart) / duration;
                Vector3 vectorDiff = multiplier * magnitude * unitVector;

                transform.position = _initPos + vectorDiff;
                timeSinceStart += Time.deltaTime;
                yield return null;
            }
            transform.position = _initPos;
        }

        _isShaking = false;
    }

    private IEnumerator ShakeRotationCoroutine(float duration, float maxAngle, int occurences)
    {

        for(int i=0; i < occurences; i++){

            maxAngle = GetRandomAngle(maxAngle);
            transform.Rotate(new Vector3(0f, 0f, maxAngle));

            float timeSinceStart = 0f;
            while (timeSinceStart < duration)
            {
                float diffAngle = - maxAngle * (Time.deltaTime / duration);
                transform.Rotate(new Vector3(0f, 0f, diffAngle));

                timeSinceStart += Time.deltaTime;
                yield return null;
            }
        }

        transform.rotation = _initRotation;
    }

    private static Vector3 GetRandomUnitVector(float magnitude){
        Vector2 _unitVector = Random.insideUnitCircle;
        _unitVector.Normalize();
        return (Vector3) _unitVector;
    }

    private static float GetRandomAngle(float angle){
        return (Random.value > 0.5f) ? angle : -angle;
    }
}
