using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    
    [SerializeField] GunData gunData;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private GameObject gun;
    private GameObject rootParent;
    private CharacterController controller;
    private float timeSinceLastShot = 0;

    private GameObject bulletHoles;

    private void Start()
    {
        rootParent = transform.parent.parent.gameObject;
        PlayerMotor.shootInput += Shoot;
        PlayerMotor.reloadInput += StartReload;
        bulletHoles = GameObject.Find("Bullet Holes");
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
            Vector3 randomizedDirecition = ShootingTarget();
            Vector3 origin = gun.transform.position + Quaternion.Euler(0,180,0)*(gun.transform.forward.normalized * 0.1f);
            //controller.detectCollisions = false;
            if (Physics.Raycast(origin, randomizedDirecition, out RaycastHit hitInfo, gunData.maxDistance))
            {
                GameObject bullet = Instantiate(bulletHole, origin, Quaternion.identity);
                Bullet bulletComponent = bullet.AddComponent<Bullet>();
                bulletComponent.origin = origin;
                bulletComponent.direction = randomizedDirecition;
                bulletComponent.bullet = bullet;
                bulletComponent.hit = hitInfo.transform.tag == "Bog";
                // Debug.Log(hitInfo.collider.gameObject);
            }
            //controller.detectCollisions = true;
            gunData.currentAmmo--;
            timeSinceLastShot = 0;
            OnGunShot();
        }
                
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.relaodTime);
        gunData.currentAmmo = gunData.maxAmmo;
        gunData.reloading = false;
    }

    private Vector3 ShootingTarget()
    {
        Vector3 direction= rootParent.transform.forward;
        Vector3 Shift = Vector3.zero;
        
        Shift.x += (float)RandomVariables.Normal(gunData.mean, gunData.stddev);
        Shift.y += (float)RandomVariables.Normal(gunData.mean, gunData.stddev);
        Shift.z += (float)RandomVariables.Normal(gunData.mean, gunData.stddev);
        
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
