using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class AreaClearState : State
{
    private Bog bog;

    private Cooldown COOLDOWN_end;

    public AreaClearState(Bog bog)
    {
        this.bog = bog;
        COOLDOWN_end = new Cooldown(3);
    }

    public void start()
    {
        bog.setAgressive(false);
        COOLDOWN_end.setStart();
    }

    public void update()
    {
        //bog.GetComponent<Animator>().GetCurrentAnimatorStateInfo();           Isto pode dar informacao sobre em qual animacao está ???

    }
}
