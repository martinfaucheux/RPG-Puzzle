using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlasher : MonoBehaviour
{
    public float flashIntensity{
        get { return GetFlashIntensity(); }
        set { SetFlashIntensity(value); }
    }

    public float flashDuration = 0.2f;

    public string materialPropertyName = "_FlashIntensity";
    private string flashShaderName = "Shader Graphs/FlashShader";

    private SpriteRenderer[] _spriteRenderers;
    private bool _isFlashing = false;

    // Start is called before the first frame update
    void Start()
    {
        // get all _sprite renderers (even inactive ones)
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
    }

    public void Flash(){
        if (!_isFlashing){
            StartCoroutine(FlashCoroutine());
        }
    }

    private float GetFlashIntensity(){
        if(_spriteRenderers != null & _spriteRenderers.Length > 0){
            return _spriteRenderers[0].material.GetFloat(materialPropertyName);
        }
        return 0f;
    }

    private void SetFlashIntensity(float value){
        if(_spriteRenderers != null){
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
            {
                if (
                    spriteRenderer.gameObject.activeSelf
                    && spriteRenderer.material.shader.name == flashShaderName
                ){
                    spriteRenderer.material.SetFloat(materialPropertyName, value);
                }
            }
        }
    }

    private IEnumerator FlashCoroutine(){
        float timeSinceStart = 0f;

        _isFlashing = true;
        while(timeSinceStart < flashDuration){
            flashIntensity = 1f - (timeSinceStart / flashDuration);
            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        flashIntensity = 0f;
        _isFlashing = false;
    }
}
