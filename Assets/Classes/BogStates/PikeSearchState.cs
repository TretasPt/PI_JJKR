using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class PikeSearchState
{
    private Bog bog;

    private Cooldown COOLDOWN_end;

    private PikeCluster[] pikeClusters;

    private int currentCluster;

    public PikeSearchState(Bog bog)
    {
        this.bog = bog;
        COOLDOWN_end = new Cooldown(bog.COOLDOWN_pike_search_end);
        pikeClusters = new PikeCluster[bog.ATTRIBUTE_maximum_number_of_pike_clusters];
        currentCluster = 0;
    }

    public void start()
    {
        COOLDOWN_end.setStart();
        spawnPikeCluster();
    }

    public void update()
    {
        if (COOLDOWN_end.done())
            bog.setState(Bog.STATE_EMPTY_SEARCH);
        followLatestPike();

        bog.checkTargetPresence();
        bog.look();
        bog.move();                                     //TODO isto terá problemas com colisões ???
    }

    private void spawnPikeCluster()
    {
                                                           //TODO Implementar
    }

    private void followLatestPike()
    {
        Vector3 vectorToDestination = pikeClusters[currentCluster].getLatestPikePosition() - bog.transform.position;
        bog.setDestinationVector(vectorToDestination);
    }
}
