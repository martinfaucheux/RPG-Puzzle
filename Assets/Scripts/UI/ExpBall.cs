using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : MonoBehaviour
{
    public static ExpBall instance;
    public Vector3 endPos;
    public float transitionDuration;
    public LTBezierPath ltPath ;
    private Material _material;

    void Awake(){
        CheckSingleton();
    }

    public void Tween(){
        TweenLinear();
        TweenAlpha();
        TweenSize();
    }

    public void TweenAlpha(){
        LeanTween.alpha((RectTransform) transform, 0, transitionDuration).setEaseInQuint();
    }

    public void TweenSize(){
        LeanTween.scale(gameObject, 0.25f * new Vector3(1, 1, 0), transitionDuration);
    }

    private Vector3[] GetBezierPoints(){
        // Vector3[] bezierPoints = new Vector3[]{
        //     new Vector3(0f,0f,0f),
        //     new Vector3(1f,0f,0f),
        //     new Vector3(1f,0f,0f),
        //     new Vector3(1f,1f,0f)
        // };

        float magnitude = (transform.position - endPos).magnitude;

        return new Vector3[]{
            transform.position,
            new Vector3(1f, 0f, 0f) * magnitude,
            new Vector3(1f, 0f, 0f) * magnitude,
            endPos,
        };
    }

    private void TweenBezier(){
        
        LTBezierPath ltPath = new LTBezierPath(GetBezierPoints());
        LeanTween.move(gameObject, ltPath, transitionDuration).setOrientToPath2d(true).setEaseOutQuint().setOnComplete(DestroyMe);
    }

    private void TweenLinear(){
        LeanTween.move(gameObject, endPos, transitionDuration).setEaseInOutQuint().setOnComplete(DestroyMe);
    }

    private void DestroyMe(){
        Destroy(gameObject);
    }

    private void CheckSingleton(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
    }
}
