using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public GameObject bog;

    public Vector3 origin;

    public Vector3 direction;

    public float speed = 200;

    public GameObject bullet;

    public bool hit;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = origin;
        bog = GameObject.FindGameObjectsWithTag("Bog")[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;

        float distanceToEnemy = (bog.transform.position - origin).magnitude;
        float distanceToBullet = (bullet.transform.position - origin).magnitude;

        if (hit && distanceToBullet > distanceToEnemy)
        {
            bog.GetComponent<Bog>().applyDamage();

            Destroy(gameObject);
        }
    }
}
