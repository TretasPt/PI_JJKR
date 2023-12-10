using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class RestingState : State
{
    private Bog bog;

    private Cooldown COOLDOWN_end;

    public RestingState(Bog bog)
    {
        this.bog = bog;
        COOLDOWN_end = new Cooldown(10);
        bog.GetComponent<AnimationManager>().rest();
    }

    public void start()
    {
        bog.setAgressive(false);
        COOLDOWN_end.setStart();
    }

    public void update()
    {
        COOLDOWN_end.count();
        if (COOLDOWN_end.done())
        {
            bog.heal();
            bog.GetComponent<AnimationManager>().wakeup();
            bog.setState(Bog.STATE_EMPTY_SEARCH);
        }
    }
}
