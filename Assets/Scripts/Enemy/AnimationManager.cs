using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        animator.SetFloat("Speed", GetComponent<CharacterController>().velocity.magnitude);     //TODO Mudar
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
