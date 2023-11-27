using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

public class PikeSearchState
{
    private Bog bog;

    private Cooldown COOLDOWN_end;

    private Cooldown COOLDOWN_nextPike;

    public PikeSearchState(Bog bog, float cooldown_end, float cooldown_next_pike)
    {
        this.bog = bog;
        COOLDOWN_end = new Cooldown(cooldown_end);
        COOLDOWN_nextPike = new Cooldown(cooldown_next_pike);
    }

    public void start()
    {
        COOLDOWN_end.setStart();
        COOLDOWN_nextPike.setStart();
    }

    public void update()
    {
        if (COOLDOWN_end.done())
            bog.setState(Bog.STATE_EMPTY_SEARCH);
        else if (COOLDOWN_nextPike.done())
            spawnNextPike();
    }

    private void spawnNextPike()
    {

    }
}
