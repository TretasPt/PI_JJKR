using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{

    public float timeToBeDestroyed;
    // Start is called before the first frame update
    void Start()
    {
     Destroy(gameObject, timeToBeDestroyed);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
