using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;
using UnityEngine;

public class PersuingState : State
{
    private Bog bog;

    private Cooldown COOLDOWN_attack;

    private Cooldown COOLDOWN_run;                      //TODO Isto é para quê?

    public PersuingState(Bog bog)
    {
        this.bog = bog;
        COOLDOWN_attack = new Cooldown(bog.COOLDOWN_attack);
    }

    public void start()
    {
        COOLDOWN_attack.setStart();
    }

    public void update()
    {
        COOLDOWN_attack.count();
        if (COOLDOWN_attack.done())
        {
            COOLDOWN_attack.setStart();
            bog.attack();
        }

        bog.checkRange();
        bog.look();
        bog.persue();
        bog.move();
    }

    private void setDestination()
    {

    }
}
