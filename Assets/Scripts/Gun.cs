using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    
    [SerializeField] GunData gunData;
    [SerializeField] private GameObject bulletHole;
    private GameObject rootParent;
    private CharacterController controller;
    private float timeSinceLastShot = 0;

    private void Start()
    {
        rootParent = transform.parent.parent.gameObject;
        PlayerMotor.shootInput += Shoot;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    private void Shoot()
    {
        //Can Shoot
        if (CanShoot())
        {
            Debug.Log("Tried to Shoot");
            Vector3 randomizedDirecition = ShootingTarget();
            Vector3 origin = transform.position + transform.forward.normalized * 2;
            //controller.detectCollisions = false;
            if (Physics.Raycast(origin, randomizedDirecition, out RaycastHit hitInfo, gunData.maxDistance))
            {
                Instantiate(bulletHole, origin + randomizedDirecition.normalized * hitInfo.distance, Quaternion.identity);
                Debug.Log("Hitted at " + hitInfo.distance);
            }
            //controller.detectCollisions = true;
            gunData.currentAmmo--;
            timeSinceLastShot = 0;
            OnGunShot();
        }
                
    }

    private Vector3 ShootingTarget()
    {
        return rootParent.transform.forward;
    }
    
    private bool CanShoot()
    {
        return gunData.currentAmmo > 0 && !gunData.reloading && timeSinceLastShot > gunData.fireRate;
    }

    private static void OnGunShot()
    {
        //TODO Implement this
    }
}
