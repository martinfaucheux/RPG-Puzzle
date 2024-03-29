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

        GameEvents.instance.onLevelUp += UpdateState;
        GameEvents.instance.onSkillEnabled += UpdateState;
    }

    void Update()
    {
        if (isShowing)
        {
            _rectTransform.RotateAround(_rectTransform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    // be compliant with event signature 
    private void UpdateState(Skill _) => UpdateState();

    private void UpdateState()
    {
        int skillPoints = SkillManager.instance.skillPoints;
        if (skillPoints > 0)
        {
            Show();
        }
        else if (skillPoints == 0)
        {
            Hide();
        }
    }

    private void Show()
    {
        _image.enabled = true;
        isShowing = true;
    }

    private void Hide()
    {
        _image.enabled = false;
        isShowing = false;
    }

    void OnDestroy()
    {
        GameEvents.instance.onLevelUp -= UpdateState;
        GameEvents.instance.onSkillEnabled -= UpdateState;
    }
}
