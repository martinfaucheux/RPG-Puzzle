using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ShaderPropertyAnimator : MonoBehaviour
{
    // NOTE: this can be used to change shader properties at runtime
    // from animations
    [SerializeField] private string _shaderAttributeName;
    [SerializeField] private float _targetValue;
    private float _previousValue;
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
        _previousValue = _material.GetFloat(_shaderAttributeName);
    }
    private void LateUpdate()
    {
        if (_targetValue != _previousValue)
        {
            _previousValue = _targetValue;
            _material.SetFloat(_shaderAttributeName, _previousValue);
        }
    }
}
