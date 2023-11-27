using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class EmptySearchState : State
{
    private Bog bog;

    private Cooldown COOLDOWN_end;

    private Cooldown COOLDOWN_nextDirection;

    private Vector3 vectorToDestination;

    private float maxAngleToDestination;

    private float angleToDestination;

    public EmptySearchState(Bog bog, float cooldown_end, float cooldown_next_direction)
    {
        this.bog = bog;
        COOLDOWN_end = new Cooldown(cooldown_end);
        COOLDOWN_nextDirection = new Cooldown(cooldown_next_direction);
    }

    public void start()
    {
        COOLDOWN_end.setStart();
        COOLDOWN_nextDirection.setStart();
        maxAngleToDestination = bog.getMaximumEmptySearchPivotAngle();
        setNextDirection();
    }

    public void update()
    {
        if (COOLDOWN_end.done())
            bog.setState(Bog.STATE_EMPTY_SEARCH);           // ALterar para estado certo
        else if (COOLDOWN_nextDirection.done())
            setNextDirection();

        bog.checkTargetPresence();
        bog.look();
        bog.move();
    }

    private void setNextDirection()
    {
        COOLDOWN_nextDirection.setStart();
        maxAngleToDestination *= 0.9f;                                                                        //TODO nao hardcoded
        angleToDestination = UnityEngine.Random.Range(-maxAngleToDestination, maxAngleToDestination);             // TODO VA
        vectorToDestination = Quaternion.Euler(0, angleToDestination, 0) * bog.transform.forward;
        vectorToDestination = vectorToDestination * 2;
        bog.setDestinationVector(vectorToDestination);
    }
}
