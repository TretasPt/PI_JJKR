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
        bog.setAgressive(false);
        COOLDOWN_end.setStart();
        spawnPikeCluster();
        bog.GetComponent<AnimationManager>().cast();
    }

    public void update()
    {
        COOLDOWN_end.count();
        if (COOLDOWN_end.done())
        {
            bog.setState(Bog.STATE_EMPTY_SEARCH);
        }
        followPike();
        
        bog.look();
        bog.move();
        bog.checkHealth();
    }

    private void spawnPikeCluster()
    {
        currentCluster++;
        if (currentCluster >= pikeClusters.Length)
        {
            pikeClusters[currentCluster % pikeClusters.Length].GetComponent<PikeCluster>().markDestroy();             //TODO só destroi o primeiro elemento
        }
        pikeClusters[currentCluster % pikeClusters.Length] = new GameObject();
        pikeClusters[currentCluster % pikeClusters.Length].AddComponent<PikeCluster>();
        pikeClusters[currentCluster % pikeClusters.Length].GetComponent<PikeCluster>().init(bog);
    }

    private void followPike()
    {
        Vector3 vectorToDestination = pikeClusters[currentCluster % pikeClusters.Length].GetComponent<PikeCluster>().getPikePosition() - bog.transform.position;
        bog.setDestinationVector(vectorToDestination);
    }
}
