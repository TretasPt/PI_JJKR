using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{

    public static Trees Instance { get; private set; }


    public GameObject objectToSpawn;

    public Vector3 origin = Vector3.zero;
    public float radius = 100;

    public int MAX_TREES = 10000;


    void Awake()
    {
        Debug.Log("Awake.");

        // PlaceTree(0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start.");

        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        int numberOfClusters = GenerateNumberOfTreeCluesters(5);

        // PlaceTree();
        GenerateTreeClusters(numberOfClusters);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private int GenerateNumberOfTreeCluesters(int max)
    {
        //TODO Make random
        Debug.Log("Returning a number of tree clusters." + max);
        return max;
    }

    private void GenerateTreeClusters(int numberOfClusters)
    //TODO
    {
        int treesToPlace = MAX_TREES;

        for (int i = numberOfClusters; i > 0; i--)
        {
            Debug.Log("Generating tree cluster - " + i);

            GameObject treeCluster = new GameObject("TreeCluster-"+i);
            treeCluster.transform.parent = transform;

            Debug.Log("Before" + treesToPlace);


            int treesPerCircle = treesToPlace / i;

            GenerateTreeCluster(treesPerCircle, treeCluster);

            treesToPlace -= treesPerCircle;

            Debug.Log("After" + treesToPlace);

        }
    }

    private void GenerateTreeCluster(int numberOfTrees, GameObject treeClusterParent)
    //TODO
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            PlaceRandomTree(treeClusterParent.transform);
        }
        Debug.Log("Generating a cluster with " + numberOfTrees + " trees.");
    }

    private void PlaceTree(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject Tree = Instantiate(objectToSpawn, position, rotation, parent);
    }

     private void PlaceTree(Vector3 position, Quaternion rotation)
    {
        PlaceTree(position,rotation,transform);
    }

    private void PlaceTree()
    {
        PlaceTree(transform.position, Quaternion.identity);
    }

    private void PlaceRandomTree(Transform parent)
    {
        Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
        randomPosition.y = 0;
        PlaceTree(randomPosition, Quaternion.identity, parent);
    }

    private void PlaceRandomTree()
    {
        Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
        randomPosition.y = 0;
        PlaceTree(randomPosition, Quaternion.identity);
    }

}
