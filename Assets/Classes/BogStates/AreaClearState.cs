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

    public AreaClearState(Bog bog)
    {
        this.bog = bog;
    }

    public void start()
    {

    }

    public void update()
    {
        //bog.GetComponent<Animator>().GetCurrentAnimatorStateInfo();           Isto pode dar informacao sobre em qual animacao está ???
    }
}
