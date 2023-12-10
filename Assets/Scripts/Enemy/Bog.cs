using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

using UnityEngine.AI;
using UnityEngine.InputSystem.Controls;

public class Bog : MonoBehaviour
{
    [NonSerialized] public const int STATE_EMPTY_SEARCH = 0;
    [NonSerialized] public const int STATE_PIKE_SEARCH = 1;
    [NonSerialized] public const int STATE_PERSUING = 2;
    [NonSerialized] public const int STATE_OUT_OF_RANGE = 3;

    [NonSerialized] private const float gravity = -20f;

    public int ATTRIBUTE_health;
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
    public float ATTRIBUTE_friction;

    public float COOLDOWN_out_of_range_end;
    public float COOLDOWN_attack;
    public float COOLDOWN_empty_search_end;
    public float COOLDOWN_empty_search_next_directon;
    public float COOLDOWN_pike_search_end; 

    public GameObject PREFAB_pikePrefab;
    
    public Collider hand;

    private GameObject target;

    private Vector3 targetVector;

    private Vector3 destinationVector;

    private int state;

    private State[] states;

    private Vector3 velocity;

    private float currentSpeed;

    void Awake()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0];
        states = new State[]
        {
            new EmptySearchState(this),
            new PikeSearchState(this),
            new PersuingState(this),
        };
        setState(STATE_EMPTY_SEARCH);
    }

    void Update()
    {
        checkHealth();
        states[state].update();
    }

    //Actions
    //  \/

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
        if ((target.transform.position - transform.position).magnitude > 5)

        velocity += destinationVector.normalized * currentSpeed;
        velocity.x /= ATTRIBUTE_friction;
        velocity.z /= ATTRIBUTE_friction;
        velocity.y += gravity * Time.deltaTime;

        float rotate = Vector3.SignedAngle(transform.forward, destinationVector, Vector3.up) * 2f * Time.deltaTime;       //TODO não fazer hardcoded
        float rotationY = transform.rotation.eulerAngles.y;

        GetComponent<CharacterController>().Move(velocity*Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, rotationY + rotate, 0);
    }
    public void attack()
    {
        if ((target.transform.position - transform.position).magnitude < 7)
        {
            GetComponent<AnimationManager>().attack();
            hand.enabled = true;
        }
    }
    public void clearObstacles()
    {
        //TODO
    }

    //  /\
    //Actions

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

    private void checkHealth()
    {
        if (ATTRIBUTE_health < 1)
            GetComponent<Animator>().SetTrigger("Attack");
    }

    public void setAgressive(bool isAgressive)
    {
        if (isAgressive)
            currentSpeed = ATTRIBUTE_run_speed;
        else
            currentSpeed = ATTRIBUTE_walk_speed;

    }

    public void takeDamage()
    {
        ATTRIBUTE_health--;
    }

    public void setState(int newState)
    {
        state = newState;
        states[state].start();
    }

    public void setDestinationVector(Vector3 destinationVector)
    {
        destinationVector.y = 0;
        this.destinationVector = destinationVector;
    }
}


/*
TODO
 - (Optional) Estado BloodyPikeSearch
 - Acabar de usar COOLDOWN_run na classe PersuingState
 - Implementar os diferentes tipos de ataque (normal, spell, salto)
 - Eliminar classe Path
 - Fazer rotation do Inimigo
 - Estado Persuing
 - Estado OutOfRange
 - Usar CharacterController em vez de RigidBody.ApplyForce()
 - Implementar attck()
 - Usar deltaTime
 - falta usar a range de visão?
*/
