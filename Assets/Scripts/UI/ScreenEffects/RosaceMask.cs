using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosaceMask : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float scaleFactor = 50f;
    public float timeToShow = 0.5f;
    public float timeToHide = 0.5f;
    public bool isShowing = false;

    public SpriteRenderer backgroundRenderer;
    private MatrixCollider _playerCollider;

    private void Start()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        _playerCollider = playerGameObject.GetComponent<MatrixCollider>();

        EnableComponents(false);

        // listen to the event
        GameEvents.instance.onEndOfLevel += Show;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    public void Show()
    {
        // no longer needed as translation is already applied by CameraTranslation script
        // transform.position = _playerCollider.GetRealPos();
        
        StartCoroutine(ResizeRosace());
        isShowing = true;
    }

    public void Hide()
    {
        StartCoroutine(FadeoutRosace());
        isShowing = false;
    }

    private IEnumerator ResizeRosace()
    {
        float maxScale = scaleFactor;
        float timeSinceStart = 0f;

        transform.localScale = Vector3.one * maxScale;
        EnableComponents(true);

        while (timeSinceStart < timeToShow)
        {
            float newScaleFactor = 1f + (maxScale - 1f) * (timeToShow - timeSinceStart) / timeToShow;
            newScaleFactor = Mathf.Max(1f, newScaleFactor);

            transform.localScale = Vector3.one * newScaleFactor;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    private IEnumerator FadeoutRosace()
    {
        float timeSinceStart = 0f;
        Color backgroundColor = backgroundRenderer.color;

        while (timeSinceStart < timeToHide * 0.99f)
        {
            float alphaValue = (timeToHide - timeSinceStart) / timeToHide;
            backgroundColor.a = alphaValue;

            backgroundRenderer.color = backgroundColor;

            timeSinceStart += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one;
        EnableComponents(false);

        // reset background color
        backgroundColor.a = 1f;
        backgroundRenderer.color = backgroundColor;
    }

    private void EnableComponents(bool value)
    {
        backgroundRenderer.enabled = value;
    }
}