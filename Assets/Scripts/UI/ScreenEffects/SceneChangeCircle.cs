using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeCircle : MonoBehaviour
{
    public static SceneChangeCircle instance = null;
    public static float initX = 1300f;
    private Vector3 _enterPos = new Vector3(initX, 0f, 0f);
    private Vector3 _centerPos = Vector3.zero;
    private Vector3 _exitPos = new Vector3(-initX, 0f, 0f);

    private RectTransform _rectTransform;
    private RectTransform _canvasRectTransform;
    private bool _hasInit = false;


    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        Init();
    }

    public void SceneStarts()
    {
        Init();

        _rectTransform.anchoredPosition = _centerPos;
        
        StartCoroutine(SlideIn(_exitPos));
    }

    public void SceneEnds()
    {
        _rectTransform.anchoredPosition = _enterPos;
        StartCoroutine(SlideIn(_centerPos));
    }

    private void Init(){

        if (!_hasInit){
            _rectTransform = GetComponent<RectTransform>();
            _canvasRectTransform = GetComponentInParent<Canvas>().gameObject.GetComponent<RectTransform>();

            AutoScale();

            _enterPos = GetStartPos();
            _exitPos = - _enterPos;
            _hasInit = true;
        }
    }

    private void AutoScale(){
        float canvasWidth = _canvasRectTransform.rect.width;
        float canvasHeight = _canvasRectTransform.rect.height;

        float radius = Mathf.Sqrt(Mathf.Pow(canvasHeight, 2) + Mathf.Pow(canvasWidth, 2f));

        _rectTransform.sizeDelta = new Vector2(radius, radius);
    }

    private Vector3 GetStartPos(){
        float canvasWidth = _canvasRectTransform.rect.width;
        float objectWidth = _rectTransform.rect.width;
        return new Vector3((canvasWidth + objectWidth) / 2f, 0f, 0f);
    }


    private IEnumerator SlideIn(Vector3 targetPos, float waitTime=0f)
    {
        yield return new WaitForSeconds(waitTime);

        float timeSinceStart = 0f;
        float transitionTime = LevelLoader.instance.transitionDuration - waitTime;
        Vector3 initPos = _rectTransform.anchoredPosition;

        while (timeSinceStart < transitionTime)
        {
            Vector3 newPos = initPos + (targetPos - initPos) * (timeSinceStart / transitionTime);
            _rectTransform.anchoredPosition = newPos;

            // use unscaledDeltaTime to allow moving when timeScale = 0 (game paused)
            timeSinceStart += Time.unscaledDeltaTime;
            yield return null;
        }

        _rectTransform.anchoredPosition = targetPos;
    }
}
