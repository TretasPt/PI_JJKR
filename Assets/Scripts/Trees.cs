using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Singleton class <c>Trees</c> represents the forest and it's generation.
/// The forest is comprised of clusters of Trees and Bushes.
/// </summary>
public class Trees : MonoBehaviour
{

    /// <value>
    /// Instance of the singleton class Trees. 
    /// </value>
    public static Trees Instance { get; private set; }

    /// <value>
    /// The prefab of a Tree object with green leafs.
    /// </value>
    public GameObject TreePrefab;

    /// <value>
    /// The prefab of a Bush object with green leafs.
    /// </value>
    public GameObject BushPrefab;

    /// <value>
    /// Material for the leafs of the central tree.
    /// </value>
    public Material RedLeafMaterial;

    /// <value>
    /// (0,0,0)
    /// Origin of the map.
    /// </value>
    public Vector3 origin = Vector3.zero;

    //TODO Remove
    public float radius = 100;

    /// <value>
    /// Maximum amount of props to be generated.
    /// </value>
    public int MAX_TREES = 10000;

    /// <value>
    /// Minimum amount of environment clusters to generate.
    /// </value>
    public int MIN_TREE_CLUSTERS = 10;

    /// <value>
    /// Maximum amount of environment clusters to generate.
    /// </value>
    public int MAX_TREE_CLUSTERS = 10;

    public float TREE_RATIO = 0.7f;





    void Awake()
    {
        Debug.Log("Awake.");

        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        int numberOfClusters = GenerateNumberOfForestCluesters(MIN_TREE_CLUSTERS, MAX_TREE_CLUSTERS);

        // PlaceTree();
        GenerateForestClusters(numberOfClusters);

    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start.");
    }

    // Update is called once per frame
    void Update()
    {

    }






    /// <summary>
    /// //TODO Make random
    /// </summary>
    /// <param name="min">Minimum amount of clusters to generate.</param>
    /// <param name="max">Minimum amount of clusters to generate.</param>
    /// <returns></returns>
    private int GenerateNumberOfForestCluesters(int min, int max)
    {
        //TODO Make random
        return (min + max) / 2;
    }

    /// <summary>
    /// Generates *numberOfClusters* forest clusters.
    /// (<c>numberOfClusters</c>).
    /// </summary>
    /// <param name="numberOfClusters"></param>
    private void GenerateForestClusters(int numberOfClusters)
    //TODO
    {
        int treesToPlace = MAX_TREES;

        for (int i = numberOfClusters; i > 0; i--)
        {

            GameObject treeCluster = new GameObject("TreeCluster-" + i);
            treeCluster.transform.parent = transform;



            int treesPerCircle = treesToPlace / i;

            GenerateForestCluster(treesPerCircle, treeCluster, TREE_RATIO);

            treesToPlace -= treesPerCircle;


        }
    }

    private void GenerateForestCluster(int numberOfTrees, GameObject treeClusterParent, float treeRatio = 0.7f)
    //TODO
    {

        float x = Random.Range(-100f, 100f);
        float z = Random.Range(-100f, 100f);

        treeClusterParent.transform.SetLocalPositionAndRotation(new Vector3(x, 0, z), Quaternion.identity);

        Debug.Log(treeClusterParent.transform.position);

        GenerateTree(origin, Quaternion.identity, treeClusterParent.transform, 2, 3, RedLeafMaterial);


        //         int treesToPlace = numberOfTrees;

        // int aaaa = 10000;
        // while (treesToPlace > 0)
        // {

        //     // int lastTree = (int)(treesToPlace * treeRatio);

        //     for (int i = 1; i < treesToPlace; i++)
        //     {
        //         float angle = Random.Range(0, 2 * Mathf.PI);
        //         float radious = Random.Range(20, 40);

        //         if(PlaceProp(PolarToCartesian(angle, radious), Quaternion.identity, treeClusterParent.transform, false)!=null){
        //             Debug.Log("Placed a tree!");
        //             treesToPlace--;
        //         }

        //     }
        //     if(aaaa--<0){
        //         Debug.Log("AAAAAAAAAAAAAAAAAA");
        //         break;
        //     }
        //     // treesToPlace--;
        // }

        // int placedTrees =0;
        // while(placedTrees<=numberOfTrees){
        //     for(int i = 0 ; i != numberOfTrees-placedTrees;i++){
        //         placedTrees++;
        //     }
        // }

        // int placedTrees = 0;
        // while (placedTrees < numberOfTrees)
        // {
        //     int placeThisCicle = numberOfTrees - placedTrees;
        //     for (int i = 0; i != placeThisCicle; i++)
        //     {
        //         placedTrees++;
        //     }
        // }
        int placedTrees = 0;
        while (placedTrees < numberOfTrees)
        {
            int placeThisCicle = numberOfTrees - placedTrees;
            int lastTree = (int)(placeThisCicle * treeRatio);
            for (int i = 0; i != placeThisCicle; i++)
            {

                float angle = Random.Range(0, 2 * Mathf.PI);
                float radious = Random.Range(20, 40);

                if (PlaceProp(PolarToCartesian(angle, radious), Quaternion.identity, treeClusterParent.transform, i<lastTree) != null)
                {
                    // Debug.Log("Placed a tree!");
                    // treesToPlace--;
                    placedTrees++;
                }

            }
        }
        Debug.Log("Placed all trees.");


        Debug.Log("Generating a cluster with " + numberOfTrees + " trees.");
    }

    private GameObject PlaceProp(Vector3 position, Quaternion rotation, Transform parent, bool isTree = true)
    {
        if (isTree)
        {
            return GenerateTree(position, rotation, parent);
        }
        else
        {
            return GenerateBush(position, rotation, parent);
        }
    }


    /// <summary>
    /// Translates a given 2d polar coordinate into a Vector3 cartesian coordinate with height 0.
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="radious"></param>
    /// <returns></returns>
    private Vector3 PolarToCartesian(float angle, float radious)
    {
        return new Vector3(Mathf.Cos(angle) * radious, 0, Mathf.Sin(angle) * radious);
    }

    /// <summary>
    /// Generate a Tree prop.
    /// </summary>
    /// <param name="position">Relative position to the parentCluster center.</param>
    /// <param name="rotation">Tree orientation. May be used to set an inclination.</param>
    /// <param name="parentCluster">Forest cluster object. The parent of this Tree</param>
    /// <param name="height">Tree height multiplier.</param>
    /// <param name="width">Tree width multiplier.</param>
    /// <returns>A Tree GameObject.</returns>
    private GameObject GenerateTree(Vector3 position, Quaternion rotation, Transform parentCluster, float height = 1, float width = 1, Material leafMaterialOverride = null)
    {

        position += parentCluster.position;

        foreach (var collider in Physics.OverlapCapsule(position, position + new Vector3(0, 10, 0), 1))
        {
            if (collider.gameObject.tag == "Tree")
            {
                Debug.Log("Skipped one.");
                return null;
            }
        }

        GameObject tree = Instantiate(TreePrefab, position, rotation, parentCluster);


        Vector3 scale = new Vector3(width, height, width);
        tree.transform.localScale = Vector3.Scale(scale, tree.transform.localScale);

        // if(Physics.CheckCapsule(position,position+new Vector3(0,10,0),5)){
        //     Debug.Log("Bati.");
        //     // return null;
        // }

        if (leafMaterialOverride)
        {
            tree.transform.Find("Sphere").GetComponent<MeshRenderer>().material = leafMaterialOverride;

        }

        return tree;

    }

    /// <summary>
    /// Generate a Bush prop.
    /// </summary>
    /// <param name="position">Relative position to the parentCluster center.</param>
    /// <param name="rotation">Bush orientation. May be used to set an inclination.</param>
    /// <param name="parentCluster">Forest cluster object. The parent of this Bush</param>
    /// <param name="height">Bush height multiplier.</param>
    /// <param name="width">Bush width multiplier.</param>
    /// <returns>A Bush GameObject.</returns>
    private GameObject GenerateBush(Vector3 position, Quaternion rotation, Transform parentCluster, float height = 1, float width = 1, Material leafMaterialOverride = null)
    {
        GameObject bush = Instantiate(BushPrefab, position + parentCluster.position, rotation, parentCluster);

        // bush.tag = "Bush";

        Vector3 scale = new Vector3(width, height, width);
        bush.transform.localScale = Vector3.Scale(scale, bush.transform.localScale);

        if (leafMaterialOverride)
        {
            bush.transform.Find("Sphere").GetComponent<MeshRenderer>().material = leafMaterialOverride;

        }

        return bush;

    }



}


