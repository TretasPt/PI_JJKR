using System;
using System.Net.NetworkInformation;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector]
    public static Action shootInput;
    public static Action reloadInput;

    private CharacterController controller;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] public bool airMovement = false;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float friction = 1.5f;
    [SerializeField] public float gravity = -35f;//-9.8f;
    [SerializeField] public float jumpHeight = 3f;
    [SerializeField] public float airMovementScaling = 0.4f;
    [SerializeField] private KeyCode reloadKey;
    private bool isGrounded;

    public GameObject Floor;



    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
        controller = GetComponent<CharacterController>();
        controller.detectCollisions = false; //temp

        bool isFreePosition = false;
        while (!isFreePosition)
        {

            isFreePosition = ResetPlayerPosition();
        }
    }

    private bool ResetPlayerPosition()
    {
        // Debug.Log("Setting player position.");
        float x = (float)RandomVariables.NormalBounded(0,5,-10,10);
        float z = (float)RandomVariables.NormalBounded(0,5,-10,10);
        GetComponent<CharacterController>().enabled = false;
        Vector3 position = Vector3.up + Floor.GetComponent<TerrainGenerator>().getGroundHeight(Vector3.zero);
        transform.SetPositionAndRotation(position, Quaternion.identity);
        GetComponent<CharacterController>().enabled = true;

        foreach (var collider in Physics.OverlapCapsule(position + new Vector3(0, -5, 0), position + new Vector3(0, 5, 0), 2))
        {
            if (collider.gameObject.CompareTag("Tree"))
            {
                return false;
            }
        }
        return true;
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

    public void ProcessShoot()
    {
        //Chekcs if left mouse button is being pressed
        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }

        if (Input.GetKeyDown(reloadKey))
        {
            reloadInput?.Invoke();
        }
    }
    //Locks the cursor in the center of the screen and makes it invisible
    public static void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //Unlocks the cursor from the center of the screen and makes it visible again
    public static void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnTriggerEnter(Collider other)
    {
        // speed = speed * -1;
        Debug.Log("Hit something T.");

    }
    void OnCollisionEnter(Collision collision)
    {
        // ContactPoint contact = collision.contacts[0];
        // Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        // Vector3 position = contact.point;
        // Instantiate(explosionPrefab, position, rotation);
        // Destroy(gameObject);
        Debug.Log("Hit something.");
    }

}
