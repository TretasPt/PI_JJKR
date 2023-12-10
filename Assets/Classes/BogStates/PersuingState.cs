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

    public PersuingState(Bog bog)
    {
        this.bog = bog;
        COOLDOWN_attack = new Cooldown(bog.COOLDOWN_attack);
    }

    public void start()
    {
        bog.setAgressive(true);
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

        bog.persue();
        bog.move();
    }
}
