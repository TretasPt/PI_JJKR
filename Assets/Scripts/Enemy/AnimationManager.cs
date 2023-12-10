using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        animator.SetFloat("Speed", GetComponent<CharacterController>().velocity.magnitude);
    }

    public void cast()
    {
        animator.SetTrigger("Cast");
    }

    public void attack()
    {
        animator.SetTrigger("Attack");
    }
}
