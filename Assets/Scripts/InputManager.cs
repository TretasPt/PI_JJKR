using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerImput playerImput;
    private PlayerImput.DefaultActions defaultActions;

    private PlayerMotor motor;
    private PlayerLook look;

    // Start is called before the first frame update
    void Awake()
    {
        playerImput = new PlayerImput();
        defaultActions = playerImput.Default;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        defaultActions.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frameES
    void FixedUpdate()
    {
        motor.ProcessMove(defaultActions.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        if (defaultActions.Look.activeControl != null)
        {
            int multiplier = defaultActions.Look.activeControl.device.ToString() == "Mouse:/Mouse" ? 1 : 6;
            look.ProcessLook(defaultActions.Look.ReadValue<Vector2>() * multiplier);
        }

    }

    private void OnEnable()
    {
        defaultActions.Enable();
    }

    private void OnDisable()
    {
        defaultActions.Disable();
    }
}
