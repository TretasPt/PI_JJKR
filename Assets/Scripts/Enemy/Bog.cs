using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

using UnityEngine.AI;

public class Bog : MonoBehaviour
{
    [NonSerialized] public const int STATE_EMPTY_SEARCH = 0;
    [NonSerialized] public const int STATE_PIKE_SEARCH = 1;
    [NonSerialized] public const int STATE_PERSUING = 2;
    [NonSerialized] public const int STATE_OUT_OF_RANGE = 3;

    public int ATTRIBUTE_maximum_number_of_pike_clusters;
    public int ATTRIBUTE_pike_cluster_capacity;
    public float ATTRIBUTE_initial_pike_spawn_distance;
    public float ATTRIBUTE_maximum_empty_search_pivot_angle;
    public float ATTRIBUTE_maximum_pike_spawn_pivot_angle;
    public float ATTRIBUTE_vision_angle;
    public float ATTRIBUTE_vision_radious;
    public float ATTRIBUTE_pike_spawn_time;
    public float ATTRIBUTE_pike_spawn_distance;
    public float ATTRIBUTE_walk_speed;
    public float ATTRIBUTE_run_speed;
    public float ATTRIBUTE_out_of_range_distance;

    public float COOLDOWN_out_of_range_end;
    public float COOLDOWN_attack;
    public float COOLDOWN_empty_search_end;
    public float COOLDOWN_empty_search_next_directon;
    public float COOLDOWN_pike_search_end; 

    public GameObject PREFAB_pikePrefab;

    private Transform head;
    private GameObject target;
    private Vector3 targetVector;
    private Vector3 destinationVector;
    private int state;
    private State[] states;

    void Awake()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0];
        states = new State[]
        {
            new EmptySearchState(this),
            new PikeSearchState(this),
            new PersuingState(this),
            new OutOfRangeState(this)
        };
        setState(STATE_EMPTY_SEARCH);
    }

    void Update()
    {
        states[state].update();
    }

    public void checkRange()
    {
        if ((transform.position - target.transform.position).magnitude > ATTRIBUTE_out_of_range_distance)
            setState(STATE_OUT_OF_RANGE);
    }
    public void look()
    {
        if (state != STATE_PERSUING && targetVisible())
            setState(STATE_PERSUING);
        else if (state == STATE_PERSUING && !targetVisible())
            setState(STATE_EMPTY_SEARCH);
    }
    public void persue()
    {
        setDestinationVector(target.transform.position - transform.position);
    }
    public void move()
    {
        Vector3 move = destinationVector.normalized*ATTRIBUTE_walk_speed;
        //GetComponent<Rigidbody>().AddForce(move, ForceMode.VelocityChange);       //TODO Delete quando poss�vel
        GetComponent<CharacterController>().Move(move*Time.deltaTime);
    }
    public void attack()
    {

    }
    public void teleport()
    {

    }

    private bool targetVisible()
    {
        targetVector = target.transform.position - transform.position;
        return targetWithinVision() && targetInFront();
    }

    private bool targetWithinVision()
    {
        return Vector3.Angle(transform.forward, targetVector) < ATTRIBUTE_vision_angle;
    }

    private bool targetInFront()
    {
        return Physics.Raycast(
            transform.position,
            targetVector,
            ATTRIBUTE_vision_radious,
            1 << target.gameObject.layer
        );
    }

    public Bog getHandle()
    {
        return this;
    }

    public void setState(int newState)
    {
        Debug.Log("New state: " + newState);
        state = newState;
        states[state].start();
    }

    public void setDestinationVector(Vector3 destinationVector)
    {
        this.destinationVector = destinationVector;
    }
}


/*
TODO
 - (Optional) Estado BloodyPikeSearch
 - Acabar de usar COOLDOWN_run na classe PersuingState
 - Estado Persuing
 - Estado OutOfRange
 - Usar CharacterController em vez de RigidBody.ApplyForce()
 - Implementar attck()
 - Usar deltaTime
 - falta usar a range de vis�o?
*/
