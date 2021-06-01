using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlurVolumeController : MonoBehaviour
{
    // NOTE: this effect does not work as intended on Android
    // it looks like default FOV is not sampled the same way than in Editor

    [SerializeField] float  disabledDofValue;

    [Tooltip("150 looks like a good value")]
    [SerializeField] float  enabledDofValue = 150f;
    [SerializeField] float  transitionDuration = 0.2f;
    [SerializeField] AnimationCurve curve;
    [SerializeField] Volume _volumeComponent;
    private DepthOfField _dof;

    private float _dofValue
    {
        get { return _dof.focalLength.value;}
        set
        {
            // _dof.focalLength.value = value;
            _dof.focusDistance.value = value;
        }
    }

    void Start(){
        VolumeProfile _volumeProfile = _volumeComponent.profile;

        if (!_volumeProfile.TryGet<DepthOfField>(out _dof))
        {
            _dof = _volumeProfile.Add<DepthOfField>(true);
        }
        _dofValue = disabledDofValue;

        GameEvents.instance.onOpenSkillMenu += EnableBlur;
        GameEvents.instance.onCloseSkillMenu += DisabledBlur;
        // TODO: add trigger for pause menu as well
    }

    public void EnableBlur(){
        _dofValue = enabledDofValue;
        _volumeComponent.enabled = true;
        // StartCoroutine(ChangeBlurCoroutine(enabledDofValue));
    }

    public void DisabledBlur(){
        _volumeComponent.enabled = false;
        // StartCoroutine(ChangeBlurCoroutine(disabledDofValue));
    }

    private IEnumerator ChangeBlurCoroutine(float targetDofValue){
        float timeSinceStart = 0f;
        float initDofValue = _dofValue;

        while(timeSinceStart < transitionDuration){

            float timeModifier = curve.Evaluate(timeSinceStart / transitionDuration);
            
            float newValue = initDofValue + (targetDofValue - initDofValue) * timeModifier;

            _dofValue = newValue;
            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        _dofValue = targetDofValue;
    }

    void OnDestroy(){
        GameEvents.instance.onOpenSkillMenu -= EnableBlur;
        GameEvents.instance.onCloseSkillMenu -= DisabledBlur;
    }
}
