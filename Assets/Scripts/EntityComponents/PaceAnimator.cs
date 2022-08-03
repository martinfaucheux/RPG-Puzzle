using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaceAnimator : MonoBehaviour
{
    private AnimatorControllerParameter _animationParameter;
    private SpriteHolder _spriteHolder;
    [SerializeField] string _animationParameterName = "bump";
    private int _paceCount = 0;

    private static string RUN_CYCLE_NAME = "runCycle";
    private bool _hasRunCycle = false;


    private Animator _animator
    {
        get
        {
            if (_spriteHolder != null)
                return _spriteHolder.activeAnimator;
            else
                return null;
        }
    }

    void Start()
    {
        _spriteHolder = GetComponent<SpriteHolder>();

        if (_animator != null)
            _animationParameter = AnimatorUtils.GetParameter(_animator, _animationParameterName);

        if (AnimatorUtils.HasParameter(_animator, RUN_CYCLE_NAME))
            _hasRunCycle = true;

    }

    public void StartPace()
    {
        if (_animationParameter != null)
        {
            if (_animationParameter.type == AnimatorControllerParameterType.Bool)
                _animator.SetBool(_animationParameterName, true);
            else if (_animationParameter.type == AnimatorControllerParameterType.Trigger)
                _animator.SetTrigger(_animationParameterName);
        }
        _paceCount++;
        if (_hasRunCycle)
            _animator.SetInteger(RUN_CYCLE_NAME, _paceCount % 2);
    }

    public void EndPace()
    {
        if (_animationParameter != null && _animationParameter.type == AnimatorControllerParameterType.Bool)
            _animator.SetBool(_animationParameter.name, false);
    }

}
