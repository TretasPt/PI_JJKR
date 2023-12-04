using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    void Update()
    {
        GetComponent<Animator>().SetFloat("Speed", GetComponent<CharacterController>().velocity.magnitude);
    }

    public void attack();
}
