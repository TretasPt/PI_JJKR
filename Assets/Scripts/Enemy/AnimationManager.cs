using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        animator.SetFloat("Speed", GetComponent<CharacterController>().velocity.magnitude);     //TODO Mudar
        AnimatorTransitionInfo info = animator.GetAnimatorTransitionInfo(0);
        //if (info.duration != 0)
        //{
        //    Debug.Log("Duration: " + info.duration);
        //    Debug.Log("Normalized Time: " + info.normalizedTime);
        //    Debug.Log("Name: " + info.nameHash);
        //}
    }

    public void cast()
    {
        animator.SetTrigger("Cast");
    }

    public void attack()
    {
        animator.SetTrigger("Attack");
    }

    public void rest()
    {
        animator.SetBool("Resting", true);
    }

    public void wakeup()
    {
        animator.SetBool("Resting", true);
    }
}
