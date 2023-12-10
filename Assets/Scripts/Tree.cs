using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Vector3 fallingDirection;
    private Quaternion rotation;
    private bool falling = false;
    private float rotationLeft;

    private void Update()
    {

    }

    public void fall(Vector3 direction)
    {
        fallingDirection = direction;
        rotation = Quaternion.LookRotation(Vector3.up, fallingDirection.normalized);        //Usar isto!!!!!!!!!!
        rotationLeft = Vector3.Angle(Vector3.up, fallingDirection);
        falling = true;
    }
}
