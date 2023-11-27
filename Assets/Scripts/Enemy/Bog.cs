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
    [NonSerialized] public const int STATE_BLOODY_PIKE_SEARCH = 2;
    [NonSerialized] public const int STATE_PERSUING = 3;
    [NonSerialized] public const int STATE_OUT_OF_RANGE = 4;

    public int ATTRIBUTE_maximum_number_of_pike_clusters;
    public int ATTRIBUTE_pike_cluster_capacity;
    public float ATTRIBUTE_initial_pike_spawn_distance;
    public float ATTRIBUTE_maximum_empty_search_pivot_angle;
    public float ATTRIBUTE_maximum_pike_spawn_pivot_angle;
    public float ATTRIBUTE_vision_angle;
    public float ATTRIBUTE_vision_radious;
    public float ATTRIBUTE_pike_spawn_time;
    public float ATTRIBUTE_pike_spawn_distance;
    public float ATTRIBUTE_walkSpeed;
    public float ATTRIBUTE_runSpeed;

    public float COOLDOWN_player_out_of_range;
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

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0];
        states = new State[]
        {
            new EmptySearchState(this),
            new PikeSearchState(this)
        };
        setState(STATE_EMPTY_SEARCH);
    }

    // Update is called once per frame
    void Update()
    {
        //states[state].update();

        states[state].update();

        //does teleport
        //does walk
        //does attack
        //does spawn pike
        //does behaviour animation
    }

    //State specific update
    private void pikeSearchUpdate()
    {

    }

    //State specific update
    private void bloodyPikeSearchUpdate()
    {

    }

    //State specific update
    private void persuingUpdate()
    {

    }

    //State specific update
    private void lostTargetUpdate()
    {

    }









    public void checkTargetPresence()
    {

    }

    public void look()
    {
        //if (targetVisible()) setState(STATE_PERSUING);                           //Este método é suposto ter mais alguma coisa ???
    }

    public void move()
    {
        Vector3 move = destinationVector.normalized*ATTRIBUTE_walkSpeed;
        GetComponent<Rigidbody>().AddForce(move, ForceMode.VelocityChange);         //TODO usar CharacterController
    }














    private bool targetVisible()                                   // Set vectorToTarget aqui ou em outra funcao?
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
        state = newState;
        states[state].start();
    }

    public void setDestinationVector(Vector3 destinationVector)
    {
        this.destinationVector = destinationVector;                                                //TODO Implementar para quando destination não se encontra na navmesh
    }
}
