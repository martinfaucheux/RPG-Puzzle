using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{

    public float rotationDuration = 0.2f;
    public Transform spritePivotTransform;

    // TODO: deprecated
    public Transform maskPivotTransform;
    // public Quaternion UpAngle;
    // public Quaternion RightAngle;
    // public Quaternion DownAngle;
    // public Quaternion LeftAngle;
    private Quaternion _initAngle;
    private bool _isAnimPlaying = false;
    private Animator _animator;

    private void Start()
    {
        _initAngle = maskPivotTransform.rotation;
        _animator = GetComponent<Animator>();
    }

    public void Trigger(Vector3 position, Direction direction)
    {
        if (!_isAnimPlaying)
        {
            // Quaternion angle = GetAngleForDirection(direction);
            // transform.rotation = angle;
            Pivot(direction);

            _isAnimPlaying = true;

            _animator.SetTrigger("attack");

            _isAnimPlaying = false;
            // TODO: remove this
            // RotateMask();
        }

    }

    private void Pivot(Direction direction)
    {
        Vector2 vect = direction.ToPos();
        Vector3 vectDirection = new Vector3(vect.x, 0, vect.y);
        spritePivotTransform.rotation = Quaternion.LookRotation(vectDirection);
    }

    // private Quaternion GetAngleForDirection(Direction directon){
    //     switch(directon.Name){
    //         case "UP": return UpAngle;
    //         case "DOWN": return DownAngle;
    //         case "LEFT": return LeftAngle;
    //         case "RIGHT": return RightAngle;
    //         default: return Quaternion.identity;
    //     }
    // }

    private void RotateMask()
    {
        StartCoroutine(RotateMaskCoroutine());
    }

    private IEnumerator RotateMaskCoroutine()
    {
        float timeSinceStart = 0f;


        while (timeSinceStart < rotationDuration)
        {
            float deltaTime = Time.deltaTime;
            float angle = 360 * deltaTime / rotationDuration;
            maskPivotTransform.RotateAround(maskPivotTransform.position, transform.forward, angle);

            timeSinceStart += deltaTime;
            yield return null;
        }

        maskPivotTransform.rotation = _initAngle;
        _isAnimPlaying = false;
    }
}
