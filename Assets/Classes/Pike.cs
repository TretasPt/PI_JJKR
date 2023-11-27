using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class Pike
{
    private GameObject pike;
    private GameObject target;
    private GameObject pikePrefab;
    private Vector3 directionVector;
    private Quaternion rotate;
    private Vector3 position;
    private Quaternion rotation;

    private float moveWaitTime;
    private float growDistance;
    private int growCapacity;
    private float maxTurnAngle;
    private float playerInfluence;

    public Pike(
        GameObject target,
        GameObject pikePrefab,
        Vector3 directionVector,
        Quaternion  rotate,
        Vector3 position,
        Quaternion rotation
    ) {
        this.target = target;
        this.pikePrefab = pikePrefab;
        this.directionVector = directionVector;
        this.rotate = rotate;
        this.position = position;
        this.rotation = rotation;

        pike = GameObject.Instantiate(pikePrefab, position, rotation);
    }
}
