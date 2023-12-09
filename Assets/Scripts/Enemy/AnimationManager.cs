using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        //float castTimePassed = animator.GetFloat("CastTime");

        animator.SetFloat("Speed", GetComponent<CharacterController>().velocity.magnitude);
        //animator.SetBool("Cast", false);
        /*
        if (castTimePassed > GetComponent<Bog>().COOLDOWN_pike_search_end)
            animator.SetBool("Cast", false);
        */
        //animator.SetFloat("CastTime", castTimePassed+Time.deltaTime);
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
