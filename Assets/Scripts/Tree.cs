using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Tree : MonoBehaviour
{
    private Vector3 fallingDirection;
    private Vector3 axis;
    private bool falling = false;
    private float rotationLeft;
    private float fallSpeed = 2;

    private void Update()
    {
        if (falling && rotationLeft > 0)
        {
            float rotate = rotationLeft * fallSpeed * Time.deltaTime;
            transform.RotateAround(transform.position, axis, rotate);
            rotationLeft -= rotate;
        }
    }

    public void fall(Vector3 direction)
    {
        fallingDirection = direction;
        //rotation = Quaternion.LookRotation(Vector3.up, fallingDirection.normalized);
        axis = Vector3.Cross(Vector3.up, fallingDirection).normalized; 
        rotationLeft = Vector3.Angle(Vector3.up, fallingDirection);
        falling = true;
    }
}
