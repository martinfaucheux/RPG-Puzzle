using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GreyScaleVolumeController : MonoBehaviour
{

    [SerializeField] float disabledSaturationValue = 0f;

    [Tooltip("150 looks like a good value")]
    [SerializeField] float enabledSaturationValue = -100f;
    [SerializeField] float transitionDuration = 0.2f;
    [SerializeField] Volume _volumeComponent;

    private ColorAdjustments _override;

    private float _saturation
    {
        get => _override.saturation.value;
        set => _override.saturation.value = value;
    }

    void Start()
    {
        VolumeProfile _volumeProfile = _volumeComponent.profile;

        if (!_volumeProfile.TryGet<ColorAdjustments>(out _override))
        {
            _override = _volumeProfile.Add<ColorAdjustments>(true);
        }
        _saturation = disabledSaturationValue;

        GameEvents.instance.onEnterState += OnEnterState;
    }

    private void OnEnterState(GameState state)
    {
        if (state == GameState.GAME_OVER)
        {
            if (transitionDuration == 0)
                SetGreyScale();
            else
                SetSmoothGreyScale();
        }
    }

    public void SetGreyScale()
    {
        _saturation = enabledSaturationValue;
        _volumeComponent.enabled = true;
    }

    public void SetSmoothGreyScale()
    {
        LeanTween.value(
            gameObject,
            value => _saturation = value,
            _saturation,
            enabledSaturationValue,
            transitionDuration
        );

    }

    public void ResetGreyScale()
    {
        _volumeComponent.enabled = false;
    }
}
