﻿using System;
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

    private float maxAngleToDestination;

    public EmptySearchState(Bog bog)
    {
        this.bog = bog;
        COOLDOWN_end = new Cooldown(bog.COOLDOWN_empty_search_end);
        COOLDOWN_nextDirection = new Cooldown(bog.COOLDOWN_empty_search_next_directon);
    }

    public void start()
    {
        bog.setAgressive(false);
        COOLDOWN_end.setStart();
        COOLDOWN_nextDirection.setStart();
        maxAngleToDestination = bog.ATTRIBUTE_maximum_empty_search_pivot_angle;
        setNextDirection();
    }

    public void update()
    {
        COOLDOWN_end.count();
        COOLDOWN_nextDirection.count();
        if (COOLDOWN_end.done())
            bog.setState(Bog.STATE_PIKE_SEARCH);
        else if (COOLDOWN_nextDirection.done())
        {
            COOLDOWN_nextDirection.setStart();
            setNextDirection();
        }

        bog.look();
        bog.move();
        bog.checkHealth();
    }

    private void setNextDirection()
    {
        maxAngleToDestination *= 0.9f;
        float angleToDestination = (float)RandomVariables.NormalBounded(0, maxAngleToDestination, -maxAngleToDestination, maxAngleToDestination+20);
        Vector3 vectorToDestination = Quaternion.Euler(0, angleToDestination, 0) * bog.transform.forward;
        vectorToDestination = vectorToDestination * 2;
        bog.setDestinationVector(vectorToDestination);
    }
}
