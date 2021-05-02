using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrateVictory : MonoBehaviour
{

    void Start()
    {
        GameEvents.instance.onEndOfLevel += Trigger;
    }

    void OnDestroy(){
        GameEvents.instance.onEndOfLevel -= Trigger;
    }

    private void Trigger(){
        StartCoroutine(TriggerCouroutine());
    }

    private IEnumerator TriggerCouroutine(){
        
        // wait for next frame to make sure the final animator is enabled
        yield return null;

        SpriteHolder _spriteHolder = GetComponentInParent<SpriteHolder>();
        _spriteHolder.activeAnimator.SetBool("celebrate", true);
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
