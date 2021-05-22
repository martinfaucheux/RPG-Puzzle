using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlurController : MonoBehaviour
{

    [SerializeField] float  disabledFocalLength;

    [Tooltip("150 looks like a good value")]
    [SerializeField] float  enabledFocalLength = 150f;
    [SerializeField] float  transitionDuration = 0.2f;
    [SerializeField] AnimationCurve curve;
    [SerializeField] Volume _volumeComponent;

    private bool _isBlurred = false;

    private DepthOfField _depthOfField;

    private float _focalLength
    {
        get { return _depthOfField.focalLength.value;}
        set
        {
            _depthOfField.focalLength.value = value;
        }
    }

    void Start(){
        VolumeProfile _volumeProfile = _volumeComponent.sharedProfile;

        if (!_volumeProfile.TryGet<DepthOfField>(out _depthOfField))
        {
            _depthOfField = _volumeProfile.Add<DepthOfField>(false);
        }

        GameEvents.instance.onOpenSkillMenu += EnableBlur;
        GameEvents.instance.onCloseSkillMenu += DisabledBlur;
        // TODO: add trigger for pause menu as well
    }

    public void EnableBlur(){
        _isBlurred = true;
        // _focalLength = enabledFocalLength;
        StartCoroutine(ChangeBlurCoroutine(enabledFocalLength));
    }

    public void DisabledBlur(){
        _isBlurred = false;
        // _focalLength = disabledFocalLength;
        StartCoroutine(ChangeBlurCoroutine(disabledFocalLength));
    }

    private IEnumerator ChangeBlurCoroutine(float targetFocalLength){
        float timeSinceStart = 0f;
        float initFocalLength = _focalLength;

        while(timeSinceStart < transitionDuration){

            float timeModifier = curve.Evaluate(timeSinceStart / transitionDuration);
            
            float newValue = initFocalLength + (targetFocalLength - initFocalLength) * timeModifier;

            _focalLength = newValue;
            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        _focalLength = targetFocalLength;
    }

    void OnDestroy(){
        GameEvents.instance.onOpenSkillMenu -= EnableBlur;
        GameEvents.instance.onCloseSkillMenu -= DisabledBlur;
    }
}
