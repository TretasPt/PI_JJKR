using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pike : MonoBehaviour
{
    public Bog bog;
    public GameObject player;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HELLO???");
        if (collision.gameObject.tag == "Player")
        {
            bog.setState(Bog.STATE_PERSUING);
            player.GetComponent<PlayerStats>().applyDamage(1);
        }
    }
}
