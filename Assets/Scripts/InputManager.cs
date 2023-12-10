using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        defaultActions.Menu.performed += ctx => OnMenuCalled();
        defaultActions.Flashlight.performed += ctx => look.FlashlightToggle();

    }

    // Update is called once per frameES
    void FixedUpdate()
    {
        motor.ProcessMove(defaultActions.Movement.ReadValue<Vector2>());
        motor.ProcessShoot();
    }

    void LateUpdate()
    {
        if (defaultActions.Look.activeControl != null && !GameMenu.optionsMenuState)
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

    private void OnMenuCalled()
    {
        GameMenu.switchGameMenu();
    }
}
