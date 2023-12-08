using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class PikeCluster : MonoBehaviour
{
    private Bog bog;

    private GameObject target;

    private GameObject[] pikes;

    private int growCapacity;

    private GameObject spawn;

    private Vector3 individualPosition;

    private Quaternion individualRotation;

    private float heightIterator;

    private bool toDestroy = false;

    public void init(Bog bog)
    {
        this.bog = bog;
        target = GameObject.FindGameObjectsWithTag("Player")[0];
        spawn = new GameObject();
        spawn.transform.position = bog.transform.position + bog.ATTRIBUTE_initial_pike_spawn_distance*(target.transform.position - bog.transform.position).normalized;
        spawn.transform.rotation = bog.transform.rotation;
        pikes = new GameObject[bog.ATTRIBUTE_pike_cluster_capacity];
        growCapacity = bog.ATTRIBUTE_pike_cluster_capacity;
        individualPosition = spawn.transform.position;
        individualRotation = spawn.transform.rotation;
        heightIterator = 2;                                                             //TODO
    }

    void Start()
    {
        StartCoroutine(growRoutine());
    }

    void Update()
    {
        if (toDestroy)
            destroy();
    }

    private IEnumerator growRoutine()
    {
        while (growCapacity > 0)
            yield return waitAndGrow();
    }

    private IEnumerator waitAndGrow()
    {
        setRotation();
        setPosition();
        GameObject pikeParent = new GameObject();
        pikeParent.transform.parent = transform;
        pikeParent.transform.position = individualPosition;
        pikeParent.transform.rotation = individualRotation;
        pikes[--growCapacity] = Instantiate(bog.PREFAB_pikePrefab, new Vector3(0,0,0), Quaternion.Euler(0,0,0), pikeParent.transform);
        pikes[growCapacity].GetComponent<Animator>().SetBool("Activate", true);
        yield return new WaitForSeconds(bog.ATTRIBUTE_pike_spawn_time);
    }

    private void setRotation()
    {
        float rotateX = UnityEngine.Random.Range(0,10);      //TODO
        float rotateY = UnityEngine.Random.Range(-bog.ATTRIBUTE_maximum_pike_spawn_pivot_angle, bog.ATTRIBUTE_maximum_pike_spawn_pivot_angle);
        float rotateZ = UnityEngine.Random.Range(-15,15);      //TODO
        float rotationToTarget = spawn.transform.rotation.y + Vector3.SignedAngle(Vector3.forward, target.transform.position - spawn.transform.position, Vector3.up);
        spawn.transform.rotation = Quaternion.Euler(0,rotationToTarget + rotateY,0);
        individualRotation = Quaternion.Euler(rotateX, rotationToTarget + rotateY, rotateZ);
    }

    private void setPosition()
    {
        Vector3 vectorToPosition = spawn.transform.rotation*(bog.ATTRIBUTE_pike_spawn_distance * transform.forward);
        spawn.transform.position = spawn.transform.position + vectorToPosition;
        individualPosition = spawn.transform.position + new Vector3(0, -bog.transform.position.y - heightIterator - 2, 0);
        heightIterator -= 2f / bog.ATTRIBUTE_pike_cluster_capacity;                                                         //TODO hardcoded
    }

    public Vector3 getLatestPikePosition()
    {
        return pikes[growCapacity].transform.position;
    }

    public void markDestroy()
    {
        toDestroy = true;
    }

    private void destroy()
    {
        StartCoroutine(destroyRoutine());
    }

    private IEnumerator destroyRoutine()
    {
        for (int i = 0; i < pikes.Length; i++)
            pikes[growCapacity].GetComponent<Animator>().SetBool("Activate", false);
        
        yield return new WaitForSeconds(bog.ATTRIBUTE_pike_spawn_time*bog.ATTRIBUTE_pike_cluster_capacity);
        
        for (int i = 0; i < pikes.Length; i++)
            Destroy(pikes[i]);
        Destroy(gameObject);
    }

    /*
    TODO 
     - Associar ao parent que, por agora, é criado mas não está ainda como parent dos pikes
     - Corrigir orientação do modelo dos pikes
     - Fazer seguir o player
    */
}
