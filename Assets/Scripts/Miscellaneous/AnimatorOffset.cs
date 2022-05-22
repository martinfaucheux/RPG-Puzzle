using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorOffset : MonoBehaviour
{

    public Animator animator { get; private set; }

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(0, -1, Random.value);
    }
}
