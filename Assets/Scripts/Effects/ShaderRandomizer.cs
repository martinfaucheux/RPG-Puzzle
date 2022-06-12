using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderRandomizer : MonoBehaviour
{
    [SerializeField] float _minValue = 0f;
    [SerializeField] float _maxValue = 1f;
    [SerializeField] string _materialProperty = "_Random";
    [SerializeField] SpriteRenderer _spriteRenderer;

    private void Start()
    {
        float value = _spriteRenderer.material.GetFloat(_materialProperty);
        _spriteRenderer.material.SetFloat(_materialProperty, value + Random.Range(_minValue, _maxValue));
    }
}
