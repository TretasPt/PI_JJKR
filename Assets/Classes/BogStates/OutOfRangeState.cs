using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OutOfRangeState : State
{
    private Bog bog;

    private Cooldown COOLDOWN_end;

    public OutOfRangeState(Bog bog)
    {
        this.bog = bog;
        COOLDOWN_end = new Cooldown(bog.COOLDOWN_out_of_range_end);
    }

    public void start()
    {
        COOLDOWN_end.setStart();
    }

    public void update()
    {
        if (COOLDOWN_end.done())
        {
            bog.teleport();
            bog.setState(Bog.STATE_EMPTY_SEARCH);
        }

        bog.checkRange();
        bog.look();
        bog.move();
        bog.attack();
    }
}
