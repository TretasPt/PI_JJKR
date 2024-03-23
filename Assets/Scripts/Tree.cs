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
    private float rotationLeft = 90;
    private float heightLeft = 30;
    private float rotationSpeed = 0.3f;
    private float sinkSpeed = 0.01f;

    private void Update()
    {
        if (falling && rotationLeft > 0)
        {
            if (rotationLeft > 0.01)
            {
                float rotate = rotationLeft * rotationSpeed * Time.deltaTime;
                transform.RotateAround(transform.position, axis, rotate);
                rotationLeft -= rotate;
            }
            if (heightLeft > 0.01)
            {
                float sink = heightLeft * sinkSpeed * Time.deltaTime;
                transform.position = transform.position + new Vector3(0, -sink, 0);
                heightLeft -= sink;
            }
        }
    }

    public void fall(Vector3 direction)
    {
        fallingDirection = direction;
        axis = Vector3.Cross(Vector3.up, fallingDirection).normalized;
        rotationLeft = 180;
        falling = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bog" && !falling)
        {
            Vector3 fallingDirection = transform.position - collider.transform.position;
            fallingDirection.y = 0;
            fall(fallingDirection);
        }
        else if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Tree Colision");
        }
    }
}
