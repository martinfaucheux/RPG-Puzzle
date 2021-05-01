using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RotatingSplashStar : MonoBehaviour
{
    public float rotationSpeed = 45f;
    public bool isShowing = false;
    private RectTransform _rectTransform;
    private Image _image;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        Hide();
        
        GameEvents.instance.onEnterLevelUp += UpdateState;
        GameEvents.instance.onSkillEnabled += UpdateState;        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowing){
            _rectTransform.RotateAround(_rectTransform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    private void UpdateState(){
        int skillPoint = SkillManager.instance.skillPoint;
        if (skillPoint > 0 && !isShowing){
            Show();
        }
        else if (skillPoint == 0 && isShowing){
            Hide();
        }
    }

    private void Show(){
        _image.enabled = true;
        isShowing = true;
    }

    private void Hide(){
        _image.enabled = false;
        isShowing = false;
    }

    void OnDestroy(){
        GameEvents.instance.onEnterLevelUp -= UpdateState;
        GameEvents.instance.onSkillEnabled -= UpdateState;
    }
}
