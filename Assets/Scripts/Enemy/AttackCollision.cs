using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AttackCollision : MonoBehaviour
{
    public GameObject bog;
    public GameObject player;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Attacked");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Attacked");
            player.GetComponent<PlayerStats>().applyDamage(5);
        }
        GetComponent<BoxCollider>().enabled = false;
    }
}
