using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Cooldown
{
    float time;
    float timePassed;

    public Cooldown(float cooldownTime)
    {
        time = cooldownTime;
        timePassed = 0; 
    }

    public void setStart()
    {
        timePassed = 0;
    }

    public void count()
    {
        timePassed += Time.deltaTime;
    }

    public bool done()
    {
        return timePassed > time;
    }

    public float getTimePassed()
    {
        return timePassed;
    }
};
