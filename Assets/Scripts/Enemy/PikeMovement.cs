using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static UnityEditor.Progress;

public class PikeMovement : MonoBehaviour
{
    private GameObject target;
    private GameObject pikePrefab;
    private Vector3 directionVector;
    private Quaternion rotate;
    private Vector3 position;       // <---- TODO Position em vez de direction, tendo sempre uma referêcnia para a posição em que há de surgir a próxima pike
    private Quaternion rotation;
    private int setting;
    private Setting[] settings =
    {
        new Setting(1f,2f,30,30f,0f),
        new Setting(0.2f,0.5f,30,30f,0f)
    };

    private struct Setting
    {
        public float moveWaitTime;
        public float growDistance;
        public int growCapacity;
        public float maxTurnAngle;
        public float playerInfluence;

        public Setting(float moveWaitTime, float growDistance, int growCapacity, float maxTurnAngle, float playerInfluence)
        {
            this.moveWaitTime = moveWaitTime;
            this.growDistance = growDistance;
            this.growCapacity = growCapacity;
            this.maxTurnAngle = maxTurnAngle;
            this.playerInfluence = playerInfluence;
        }
    }

    public void setParameters(int setting)
    {
        this.setting = setting;
    }

    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0];
        pikePrefab = GameObject.FindGameObjectsWithTag("PikeOriginal")[0];
        directionVector = new Vector3(0, 0, 0);
        rotate = Quaternion.Euler(0, 0, 0);
        position = transform.position + new Vector3(0,-3f,0);
        rotation = transform.rotation;
        StartCoroutine(growRoutine());
    }

    private IEnumerator growRoutine()
    {
        while (settings[setting].growCapacity > 0)
        {
            settings[setting].growCapacity--;
            yield return waitAndGrow();
        }
    }

    private IEnumerator waitAndGrow()
    {
        setRotate();
        setDirectionVector();
        setRotation();
        setPosition();
        GameObject newPike = Instantiate(pikePrefab, position, rotation);
        newPike.transform.parent = transform;
        yield return new WaitForSeconds(settings[setting].moveWaitTime);
    }

    private void setRotate()
    {
        rotate = Quaternion.AngleAxis(Random.Range(-settings[setting].maxTurnAngle, settings[setting].maxTurnAngle), Vector3.up);
    }

    private void setDirectionVector()
    {
        directionVector = target.transform.position - position;
        directionVector.y = 0;
        directionVector = rotate * (directionVector.normalized * settings[setting].growDistance);
    }
    private void setRotation()
    {
        rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, directionVector, Vector3.up), 0);
    }

    private void setPosition()
    {
        position = position + (rotate * directionVector);
    }
}
