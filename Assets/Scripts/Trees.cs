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

    public int MAX_TREE_CLUSTERS = 10;


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

        int numberOfClusters = GenerateNumberOfTreeCluesters(MAX_TREE_CLUSTERS);

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

        float x = Random.Range(-100f,100f);
        float z = Random.Range(-100f,100f);
        
        // treeClusterParent.transform.position.Set(x,0,z);
        treeClusterParent.transform.SetLocalPositionAndRotation(new Vector3(x,0,z),Quaternion.identity);

        Debug.Log(treeClusterParent.transform.position);

        for (int i = 0; i < numberOfTrees; i++)
        {
            // PlaceRandomTree(treeClusterParent.transform);
            float angle = Random.Range(0,2*Mathf.PI);
            float radious = Random.Range(20,40);

            PlaceTree(PolarToCartesian(angle,radious)+treeClusterParent.transform.position,Quaternion.identity, treeClusterParent.transform);
            
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
        // Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
                Vector3 randomPosition = parent.position + Random.insideUnitSphere * radius;
        randomPosition.y = 0;
        PlaceTree(randomPosition, Quaternion.identity, parent);
    }

    private void PlaceRandomTree()
    {
        Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
        randomPosition.y = 0;
        PlaceTree(randomPosition, Quaternion.identity);
    }

    //
    // Summary:
    //     Returns the Vector3 position of a tree 
    //
    // Parameters:
    //   f:
    private Vector3 PolarToCartesian(float angle, float radious){
        return new Vector3(Mathf.Cos(angle)*radious,0,Mathf.Sin(angle)*radious);
    }

}
