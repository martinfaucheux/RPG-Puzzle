using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCounterWizzer : MonoBehaviour
{
    public static SkillCounterWizzer instance;

    public float period = 1f;
    public float amplitude = 1f;
    public int occurences = 1;

    private bool _isWizzing = false;
    private RectTransform _rectTransform;
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

    void Start(){
        _rectTransform = GetComponent<RectTransform>();
        _initPos = _rectTransform.anchoredPosition;
    }

    public void Wizz(){
        if(!_isWizzing){
            StartCoroutine(WizzCoroutine());
        }
    }

    private IEnumerator WizzCoroutine(){
        _isWizzing = true;
        for(int i = 0; i < occurences; i++){
            float timeSinceStart = 0f;
            while(timeSinceStart < period){
                
                float xDisplacement = amplitude * Mathf.Sin(2 * Mathf.PI * timeSinceStart / period);
                Vector3 newPos = _initPos + new Vector3(xDisplacement, 0f, 0f);
                _rectTransform.anchoredPosition = newPos;
                timeSinceStart += Time.deltaTime;
                yield return null;
            }
            _rectTransform.anchoredPosition = _initPos;
        }
        _isWizzing = false;

    }
}
