using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : UIBarElement
{
    [SerializeField] bool isFull = false;
    // sprite images
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    private Animator _animator;

    void Start(){
        _animator = GetComponent<Animator>();
        PlayPulseAnim();
    }

    public override void SetValue(bool value)
    {
        if (value)
        {
            imageComponent.sprite = fullHeartSprite;
        }
        else
        {
            imageComponent.sprite = emptyHeartSprite;
        }
    }

    public void SetToFull()
    {
        imageComponent.sprite = fullHeartSprite;
        if (!isFull)
            PlayPulseAnim();
        isFull = true;
    }

    public void SetToEmpty()
    {
        imageComponent.sprite = emptyHeartSprite;
        if (isFull)
            PlayPulseAnim();
        isFull = false;
    }

    private void PlayPulseAnim(){
        // animator can be null if this is instantiated at the start of the scene
        if (_animator != null)
            _animator.SetTrigger("pulse");
    }
}
