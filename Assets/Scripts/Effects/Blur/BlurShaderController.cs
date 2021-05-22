using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurShaderController : MonoBehaviour
{

    // based on tutorial here
    // https://www.youtube.com/watch?v=cjfNgra1uWk
    // this work only for first frame

    [SerializeField] Camera blurCamera;
    [SerializeField] Material blurMaterial;

    [SerializeField] string shaderRenTextProperty = "_RenTex";

    
    void Start()
    {
        if(blurCamera.targetTexture != null){
            blurCamera.targetTexture.Release();
        }
        blurCamera.targetTexture = new RenderTexture(
            Screen.width,
            Screen.height,
            24,
            RenderTextureFormat.ARGB1555,
            1
        );
        blurMaterial.SetTexture(shaderRenTextProperty, blurCamera.targetTexture);

    }
}
