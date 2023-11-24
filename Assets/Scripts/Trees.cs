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

    public GameObject Floor;

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
    /// Generate number of tree clusters to be generated.
    /// </summary>
    /// <param name="min">Minimum amount of clusters to generate.</param>
    /// <param name="max">Minimum amount of clusters to generate.</param>
    /// <returns></returns>
    private int GenerateNumberOfForestCluesters(int min, int max)
    {
        //TODO Make random - descrete uniform
        return (min + max) / 2;
    }

    /// <summary>
    /// Generates <c>numberOfClusters</c> forest clusters.
    /// </summary>
    /// <param name="numberOfClusters">Amount of forest clusters to be generated.</param>
    private void GenerateForestClusters(int numberOfClusters)
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

    /// <summary>
    /// Generate a forest cluster composed of a central tree and <c>numberOfTrees</c> trees and bushes around it.
    /// </summary>
    /// <param name="numberOfTrees">Amount of forest props to generate(Trees and Bushes).</param>
    /// <param name="treeClusterParent">Parent "forest".</param>
    /// <param name="treeRatio">It generates <c>treeRatio</c> trees and 1-<c>treeRatio</c> bushes.</param>
    private void GenerateForestCluster(int numberOfTrees, GameObject treeClusterParent, float treeRatio = 0.7f)
    {

        //TODO Make random - use uniform.
        float x = Random.Range(-Floor.transform.localScale.x / 2, Floor.transform.localScale.x / 2);
        //TODO Make random - use uniform.
        float z = Random.Range(-Floor.transform.localScale.z / 2, Floor.transform.localScale.z / 2);

        treeClusterParent.transform.SetLocalPositionAndRotation(new Vector3(x, 0, z), Quaternion.identity);

        GameObject centralTree = GenerateTree(origin, Quaternion.identity, treeClusterParent.transform, 2, 3, RedLeafMaterial);

        centralTree.name = "CentralTree";

        int placedTrees = 1;
        while (placedTrees < numberOfTrees)
        {
            int placeThisCicle = numberOfTrees - placedTrees;
            int lastTree = (int)(placeThisCicle * treeRatio);
            for (int i = 0; i != placeThisCicle; i++)
            {
                //TODO Make random - use uniform.
                float angle = Random.Range(0, 2 * Mathf.PI);
                //TODO Make random - use normal.
                float radious = Random.Range(20, 40);

                if (PlaceProp(PolarToCartesian(angle, radious), Quaternion.identity, treeClusterParent.transform, i < lastTree) != null)
                {
                    placedTrees++;
                }

            }
        }

    }

    /// <summary>
    /// Generates a new tree or bush.
    /// </summary>
    /// <param name="position">Relative position to the parentCluster center.</param>
    /// <param name="rotation">Tree orientation. May be used to set an inclination.</param>
    /// <param name="parent">Forest cluster object. The parent of this Tree.</param>
    /// <param name="isTree">True generates a tree, false generates a bush.</param>
    /// <returns>The generated object or null if it fails.</returns>
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
    /// <returns>A Tree GameObject or null if it fails.</returns>
    private GameObject GenerateTree(Vector3 position, Quaternion rotation, Transform parentCluster, float height = 1, float width = 1, Material leafMaterialOverride = null)
    {

        position += parentCluster.position;

        if (!isInsideMap(position))
            return null;


        foreach (var collider in Physics.OverlapCapsule(position, position + new Vector3(0, 10, 0), 1))
        {
            if (collider.gameObject.CompareTag("Tree"))
            {
                return null;
            }
        }

        GameObject tree = Instantiate(TreePrefab, position, rotation, parentCluster);


        Vector3 scale = new Vector3(width, height, width);
        tree.transform.localScale = Vector3.Scale(scale, tree.transform.localScale);

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

        position += parentCluster.position;


        if (!isInsideMap(position))
            return null;

        GameObject bush = Instantiate(BushPrefab, position, rotation, parentCluster);

        // bush.tag = "Bush";

        Vector3 scale = new Vector3(width, height, width);
        bush.transform.localScale = Vector3.Scale(scale, bush.transform.localScale);

        if (leafMaterialOverride)
        {
            bush.transform.Find("Sphere").GetComponent<MeshRenderer>().material = leafMaterialOverride;

        }

        return bush;

    }

    private Boolean isInsideMap(Vector3 position)
    {
        Vector3 floorTransform = Floor.transform.lossyScale;//May be better to use localScale
        return position.x < floorTransform.x / 2 && position.z < floorTransform.z / 2 && position.x > -floorTransform.x / 2 && position.z > -floorTransform.z / 2;
    }


}


