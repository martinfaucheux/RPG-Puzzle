using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    public float defaultShakeDuration = 0.5f;
    public float defaultShakeMagnitude = 0.5f;
    // public float defaultShakeAngle = 1f;
    public bool defaultFadingEnabled = true;

    private bool _isShaking = false;
    private Vector3 _initPos;

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
        GameEvents.instance.onPlayerGetDamage += ShakeOnce;
    }

    private void Update()
    {
        // TODO: remove
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeOnce();
        }
    }

    public void ShakeOnce()
    {
        ShakeOnce(defaultShakeDuration, defaultShakeMagnitude);
    }

    public void ShakeOnce(float duration, float magnitude)
    {
        if (!_isShaking)
        {
            StartCoroutine(ShakeOnceCoroutine(duration, magnitude));
        
        //     if (defaultShakeAngle > 0f)
        //     {
        //         StartCoroutine(ShakeRotationCoroutine(duration, defaultShakeAngle));
        //     }
        }
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude, bool fadingEnabled)
    {
        _isShaking = true;
        float timeSinceStart = 0f;
        while(timeSinceStart < duration)
        {
            float multiplier = fadingEnabled ? (1f - timeSinceStart / duration) : 1f;
            float x = magnitude * multiplier * Random.Range(-1f, 1f);
            float y = magnitude * multiplier * Random.Range(-1f, 1f);
            Vector3 vectorDiff = new Vector3(x, y, 0f);

            transform.position = _initPos + vectorDiff;
            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        _isShaking = false;
        transform.position = _initPos;
    }

    private IEnumerator ShakeOnceCoroutine(float duration, float magnitude)
    {
        // get a random unit Vector2
        Vector2 _unitVector = Random.insideUnitCircle;
        _unitVector.Normalize();
        // make it Vector3
        Vector3 unitVector = _unitVector;

        _isShaking = true;
        float timeSinceStart = 0f;
        while(timeSinceStart < duration)
        {
            float multiplier = (duration - timeSinceStart) / duration;
            Vector3 vectorDiff = multiplier * magnitude * unitVector;

            transform.position = _initPos + vectorDiff;
            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        _isShaking = false;
        transform.position = _initPos;
    }

    // private IEnumerator ShakeRotationCoroutine(float duration, float maxAngle)
    // {
    //     maxAngle = (Random.value > 0.5f) ? maxAngle : -maxAngle;
    //     transform.Rotate(new Vector3(0f, 0f, maxAngle));

    //     float timeSinceStart = 0f;
    //     while (timeSinceStart < duration)
    //     {
    //         float diffAngle = - maxAngle * (Time.deltaTime / duration);
    //         transform.Rotate(new Vector3(0f, 0f, diffAngle));

    //         timeSinceStart += Time.deltaTime;
    //         yield return null;
    //     }

    //     transform.rotation = Quaternion.identity;
    // }
}
