using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrees : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            Vector3 fallingDirection = collision.transform.position - transform.position;
            fallingDirection.y = 0;
            collision.gameObject.GetComponent<Tree>().fall(fallingDirection);
        }
    }
}
