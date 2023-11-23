using System.Net.NetworkInformation;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] public bool airMovement = false;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float friction = 1.5f;
    [SerializeField] public float gravity = -35f;//-9.8f;
    [SerializeField] public float jumpHeight = 3f;
    [SerializeField] public float airMovementScaling = 0.4f;
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {



        if (isGrounded || airMovement)
        {
            playerVelocity.x += input.x * speed;
            playerVelocity.z += input.y * speed;
            playerVelocity.x /= friction;
            playerVelocity.z /= friction;
        }
        if (!isGrounded)
        {
            playerVelocity.y += gravity * Time.deltaTime;
            playerVelocity.x += input.x * speed * airMovementScaling;
            playerVelocity.z += input.y * speed * airMovementScaling;
            //airFriction is the same as ground friction. It is working well :)
            playerVelocity.x /= friction;
            playerVelocity.z /= friction;
        }





        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        controller.Move(transform.TransformDirection(playerVelocity) * Time.deltaTime);


    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

}
