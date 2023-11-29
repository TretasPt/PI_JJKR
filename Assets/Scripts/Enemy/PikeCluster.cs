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

    private GameObject[] pikes;

    private int growCapacity;

    private GameObject spawn;

    public void init(Bog bog)
    {
        this.bog = bog;
        spawn = new GameObject();
        spawn.transform.position = bog.transform.position + bog.ATTRIBUTE_initial_pike_spawn_distance*bog.transform.forward;
        spawn.transform.rotation = bog.transform.rotation;
        pikes = new GameObject[bog.ATTRIBUTE_pike_cluster_capacity];
        growCapacity = bog.ATTRIBUTE_pike_cluster_capacity;
    }

    void Start()
    {
        StartCoroutine(growRoutine());
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
        pikes[--growCapacity] = Instantiate(bog.PREFAB_pikePrefab, spawn.transform.position, spawn.transform.rotation, transform);
        //pikes[growCapacity].transform.parent = transform;
        yield return new WaitForSeconds(bog.ATTRIBUTE_pike_spawn_time);
    }

    private void setRotation()
    {
        float rotateY = Random.Range(-bog.ATTRIBUTE_maximum_pike_spawn_pivot_angle, bog.ATTRIBUTE_maximum_pike_spawn_pivot_angle);
        Quaternion rotate = Quaternion.AngleAxis(rotateY, Vector3.up);
        spawn.transform.rotation = spawn.transform.rotation * rotate;
    }

    private void setPosition()
    {
        Vector3 vectorToPosition = spawn.transform.rotation*(bog.ATTRIBUTE_pike_spawn_distance * transform.forward);
        spawn.transform.position = spawn.transform.position + vectorToPosition;
    }

    public Vector3 getLatestPikePosition()
    {
        return pikes[growCapacity].transform.position;
    }

    public void destroy()
    {
        for (int i = 0; i < pikes.Length; i++)
            Destroy(pikes[i]);
        Destroy(gameObject);
    }


    /*
    TODO 
     - Associar ao parent que, por agora, é criado mas não está ainda como parent dos pikes
     - Corrigir orientação do modelo dos pikes
    */
}
