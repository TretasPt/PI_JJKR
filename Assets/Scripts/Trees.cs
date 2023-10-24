using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trees : MonoBehaviour
{

    public static Trees Instance { get; private set; }


    public GameObject TreePrefab;

    public GameObject BushPrefab;

    public Material RedLeafMaterial;

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

        GameObject centralTree = PlaceProp(treeClusterParent.transform.position, Quaternion.identity, treeClusterParent.transform);
        // Debug.Log(centralTree.transform.position);
        // centralTree.GetComponent("Sphere").AddComponent();;
        // centralTree.GetComponent<MeshRenderer>().material = RedLeafMaterial;
        // centralTree.GetComponent("Sphere").GetComponent<MeshRenderer>().material = RedLeafMaterial;
        centralTree.transform.Find("Sphere").GetComponent<MeshRenderer>().material = RedLeafMaterial;

        for (int i = 1; i < numberOfTrees; i++)
        {
            // PlaceRandomTree(treeClusterParent.transform);
            float angle = Random.Range(0,2*Mathf.PI);
            float radious = Random.Range(20,40);

            Boolean isTree = Random.Range(0f,1f) > 0.5;

            PlaceProp(PolarToCartesian(angle,radious)+treeClusterParent.transform.position,Quaternion.identity, treeClusterParent.transform, isTree);
            
        }
        Debug.Log("Generating a cluster with " + numberOfTrees + " trees.");
    }

    private GameObject PlaceProp(Vector3 position, Quaternion rotation, Transform parent, Boolean isTree=true)
    {
        return Instantiate(isTree?TreePrefab:BushPrefab, position, rotation, parent);
    }

     private GameObject PlaceProp(Vector3 position, Quaternion rotation, Boolean isTree=true)
    {
        return PlaceProp(position,rotation,transform,isTree);
    }

    private GameObject PlaceProp( Boolean isTree=true)
    {
        return PlaceProp(transform.position, Quaternion.identity, isTree);
    }

    private GameObject PlaceRandomProp(Transform parent, Boolean isTree=true)
    {
        // Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
                Vector3 randomPosition = parent.position + Random.insideUnitSphere * radius;
        randomPosition.y = 0;
        return PlaceProp(randomPosition, Quaternion.identity, parent, isTree);
    }

    private GameObject PlaceRandomProp( Boolean isTree=true)
    {
        Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
        randomPosition.y = 0;
        return PlaceProp(randomPosition, Quaternion.identity, isTree);
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
