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

        Animator activeAnimator = GetComponentInParent<SpriteHolder>().activeAnimator;
        activeAnimator.SetBool("celebrate", true);

        // replace the different sprite parts by the unique "celebrate" sprite
        // at the end of movement
        yield return new WaitForSeconds(GameManager.instance.actionDuration);
        while (!activeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Celebrate")){
            yield return null;
        }
        foreach(Transform childTransform in activeAnimator.transform){
            childTransform.gameObject.SetActive(false);
        }
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
