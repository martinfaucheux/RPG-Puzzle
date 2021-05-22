using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlurVolumeController : MonoBehaviour
{
    // NOTE: this effect does not work as intended on Android
    // it looks like default FOV is not sampled the same way than in Editor

    [SerializeField] float  disabledFocalLength;

    [Tooltip("150 looks like a good value")]
    [SerializeField] float  enabledFocalLength = 150f;
    [SerializeField] float  transitionDuration = 0.2f;
    [SerializeField] AnimationCurve curve;
    [SerializeField] Volume _volumeComponent;
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
        _focalLength = disabledFocalLength;

        GameEvents.instance.onOpenSkillMenu += EnableBlur;
        GameEvents.instance.onCloseSkillMenu += DisabledBlur;
        // TODO: add trigger for pause menu as well
    }

    public void EnableBlur(){
        StartCoroutine(ChangeBlurCoroutine(enabledFocalLength));
    }

    public void DisabledBlur(){
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