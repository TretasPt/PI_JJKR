using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

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
    public static float SampleGaussian(Random random, float mean, float stddev)
    {
        // converts uniform random of (0,1] to [0,1)
        float x1 = (float)(1 - random.NextDouble());
        float x2 = (float)(1 - random.NextDouble());

        float y1 = Mathf.Sqrt(-2.0f * Mathf.Log(x1)) * Mathf.Cos(2.0f * Mathf.PI * x2);
        return y1 * stddev + mean;
    }
    private Vector3 ShootingTarget()
    {
        Random rand = new Random();
        
        Vector3 direction= rootParent.transform.forward;
        Vector3 Shift = Vector3.zero;
        
        Shift.x += SampleGaussian(rand, gunData.mean, gunData.stddev); 
        Shift.y += SampleGaussian(rand, gunData.mean, gunData.stddev); 
        Shift.z += SampleGaussian(rand, gunData.mean, gunData.stddev); 
        
        direction += Shift;
        return direction;
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
