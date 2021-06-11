using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    public float textFadePeriod = 3f;
    [Tooltip("Time after which we show the text")]
    public float showDelay = 1f;
    private bool _isShowing = false;
    private Text _textComponent;

    // time at which we start showing the text
    private float _showTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // once enabled periodically fade in the "Press R to restart" text
        if(_isShowing){
            Color color = _textComponent.color;
            float timeOffset = Time.time - _showTime;
            color.a = (1f - Mathf.Cos(timeOffset * 2f * Mathf.PI / textFadePeriod)) / 2f;
            _textComponent.color = color;
        }
    }

    protected void DelayShow(){
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine(){
        _textComponent = GetComponent<Text>();
        // wait for specified time before showing
        yield return new WaitForSeconds(showDelay);
        _showTime = Time.time;
        _textComponent.enabled = true;
        _isShowing = true;

    }
}
