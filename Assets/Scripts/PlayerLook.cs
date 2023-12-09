using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerLook : MonoBehaviour
{

    public Camera cam;
    private float yRotation = 0f;

    public static float xSensitivity = 30f;
    public static float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        yRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        yRotation = Mathf.Clamp(yRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(yRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX * Time.deltaTime * xSensitivity);

    }

    public void FlashlightToggle()
    {
        Light light = transform.GetChild(0).GetChild(1).GetComponentInChildren<Light>();
        light.enabled = !light.enabled;
        // Debug.Log(light.enabled);
    }

    public static Vector2 getSensitivi2D()
    {
        return new Vector2(xSensitivity, ySensitivity);
    }

}
