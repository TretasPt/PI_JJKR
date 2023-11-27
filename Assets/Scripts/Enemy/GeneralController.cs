using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class BogGeneralController : MonoBehaviour
{
    public GameObject target;
    public Animator animator;
    public GameObject pikePrefab;
    public float visionRadious;

    private NavMeshAgent agent;
    private bool persuing;
    private DateTime lostPlayerTime;
    private DateTime pikeCooldown;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        persuing = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Debug.Log(Convert.ToInt32(false));
            GetComponent<Rigidbody>().drag = 0;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0), ForceMode.VelocityChange);
        }

        if (targetInSight())
        {
            agent.SetDestination(target.transform.position);
            persuing = true;
            lostPlayerTime = DateTime.Now;
        }
        else if (targetLost())
        {
            if (pikeCooldownDone())
                spawnPike();
            pikeCooldown = DateTime.Now;
            persuing = false;
        }
    }

    private bool targetInSight()
    {
        return Physics.Raycast(
            transform.position,
            target.transform.position - transform.position,
            visionRadious,
            1 << target.gameObject.layer
        );
    }

    private bool targetLost()
    {
        return persuing && (DateTime.Now - lostPlayerTime).TotalSeconds > 1;
    }

    private void spawnPike()
    {
        GameObject pike = new GameObject("Pike");
        (pike.AddComponent(typeof(PikeMovement)) as PikeMovement).setParameters(0);
        pike.transform.position = transform.position + (new Vector3(0, 2f, 0));
        pike.transform.rotation = transform.rotation;
    }

    private bool pikeCooldownDone()
    {
        return (DateTime.Now - pikeCooldown).TotalSeconds > 60;
    }
}
