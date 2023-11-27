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

    private PikeCluster[][] pikes;

    public PikeSearchState(Bog bog)
    {
        this.bog = bog;
        COOLDOWN_end = new Cooldown(bog.COOLDOWN_pike_search_end);
        COOLDOWN_nextPike = new Cooldown(bog.COOLDOWN_pike_search_next_pike);
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
            spawnNextPikeCluster();

        bog.checkTargetPresence();
        bog.look();
        bog.move();                                     //TODO isto terá problemas com colisões ???
    }

    private void spawnNextPikeCluster()
    {

    }
}
