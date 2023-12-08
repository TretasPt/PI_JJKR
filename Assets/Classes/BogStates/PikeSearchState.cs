using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class PikeSearchState : State
{
    private Bog bog;

    private Cooldown COOLDOWN_end;

    private GameObject[] pikeClusters;

    private int currentCluster;

    public PikeSearchState(Bog bog)
    {
        this.bog = bog;
        COOLDOWN_end = new Cooldown(bog.COOLDOWN_pike_search_end);
        pikeClusters = new GameObject[bog.ATTRIBUTE_maximum_number_of_pike_clusters];
        currentCluster = -1;
    }

    public void start()
    {
        COOLDOWN_end.setStart();
        spawnPikeCluster();
    }

    public void update()
    {
        COOLDOWN_end.count();
        if (COOLDOWN_end.done())
            bog.setState(Bog.STATE_EMPTY_SEARCH);
        followLatestPike();

        bog.checkRange();
        bog.look();
        bog.move();
    }

    private void spawnPikeCluster()
    {
        currentCluster++;
        if (currentCluster == pikeClusters.Length)
        {
            currentCluster = 0;
            pikeClusters[currentCluster].GetComponent<PikeCluster>().destroy();             //TODO só destroi o primeiro elemento
        }
        pikeClusters[currentCluster] = new GameObject();
        pikeClusters[currentCluster].AddComponent<PikeCluster>();
        pikeClusters[currentCluster].GetComponent<PikeCluster>().init(bog);
    }

    private void followLatestPike()
    {
        Vector3 vectorToDestination = pikeClusters[currentCluster].GetComponent<PikeCluster>().getLatestPikePosition() - bog.transform.position;
        bog.setDestinationVector(vectorToDestination);
    }
}
