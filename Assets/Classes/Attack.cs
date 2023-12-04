using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Attack
{
    private GameObject hitbox;
    
    private Animator animator;

    private float activeTimeStart;

    private float activeTimeEnd;

    private float timer;

    public Attack(GameObject hitbox, Animator animator, float activeTimeStart, float activeTimeEnd)
    {
        this.hitbox = hitbox;
        this.animator = animator;
        this.activeTimeStart = activeTimeStart;
        this.activeTimeEnd = activeTimeEnd;
        timer = 0;
    }

    public void start()
    {

    }

    public void count()
    {

    }
}
