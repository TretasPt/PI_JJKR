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

    public PikeCluster(Bog bog)
    {
        transform.position = bog.transform.position + bog.transform.forward*bog.ATTRIBUTE_initial_pike_spawn_distance;
        transform.rotation = bog.transform.rotation;
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
        {
            growCapacity--;
            yield return waitAndGrow();
        }
    }

    private IEnumerator waitAndGrow()
    {
        setRotation();
        setPosition();
        pikes[growCapacity]  = Instantiate(bog.PREFAB_pikePrefab, transform.position, transform.rotation);
        pikes[growCapacity].transform.parent = transform;
        yield return new WaitForSeconds(bog.ATTRIBUTE_pike_spawn_time);
    }

    private void setRotation()
    {
        float rotateY = Random.Range(-bog.ATTRIBUTE_maximum_pike_spawn_pivot_angle, bog.ATTRIBUTE_maximum_pike_spawn_pivot_angle);
        Quaternion rotate = Quaternion.AngleAxis(rotateY, Vector3.up);
        transform.rotation = transform.rotation * rotate;
    }

    private void setPosition()
    {
        transform.position = transform.forward * bog.ATTRIBUTE_pike_spawn_distance;
    }
}
