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
        Debug.Log("Returning a number of tree clusters." + max);
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

            GenerateForestCluster(treesPerCircle, treeCluster);

            treesToPlace -= treesPerCircle;


        }
    }

    private void GenerateForestCluster(int numberOfTrees, GameObject treeClusterParent)
    //TODO
    {

        float x = Random.Range(-100f, 100f);
        float z = Random.Range(-100f, 100f);

        // treeClusterParent.transform.position.Set(x,0,z);
        treeClusterParent.transform.SetLocalPositionAndRotation(new Vector3(x, 0, z), Quaternion.identity);

        Debug.Log(treeClusterParent.transform.position);

        // GameObject centralTree = PlaceProp(treeClusterParent.transform.position, Quaternion.identity, treeClusterParent.transform);
        GameObject centralTree = PlaceProp(origin, Quaternion.identity, treeClusterParent.transform);
        // Debug.Log(centralTree.transform.position);
        // centralTree.GetComponent("Sphere").AddComponent();;
        // centralTree.GetComponent<MeshRenderer>().material = RedLeafMaterial;
        // centralTree.GetComponent("Sphere").GetComponent<MeshRenderer>().material = RedLeafMaterial;
        centralTree.transform.Find("Sphere").GetComponent<MeshRenderer>().material = RedLeafMaterial;

        for (int i = 1; i < numberOfTrees; i++)
        {
            // PlaceRandomTree(treeClusterParent.transform);
            float angle = Random.Range(0, 2 * Mathf.PI);
            float radious = Random.Range(20, 40);

            bool isTree = Random.Range(0f, 1f) > 0.5;

            PlaceProp(PolarToCartesian(angle, radious), Quaternion.identity, treeClusterParent.transform, isTree);

        }
        Debug.Log("Generating a cluster with " + numberOfTrees + " trees.");
    }

    private GameObject PlaceProp(Vector3 position, Quaternion rotation, Transform parent, bool isTree = true)
    {
        // return Instantiate(isTree ? TreePrefab : BushPrefab, position, rotation, parent);
        if (isTree)
        {
            return GenerateTree(position, rotation, parent);
        }
        else
        {
            return GenerateBush(position, rotation, parent);
        }
    }

    // private GameObject PlaceProp(Vector3 position, Quaternion rotation, bool isTree = true)
    // {
    //     return PlaceProp(position, rotation, transform, isTree);
    // }

    // private GameObject PlaceProp(bool isTree = true)
    // {
    //     return PlaceProp(transform.position, Quaternion.identity, isTree);
    // }

    // private GameObject PlaceRandomProp(Transform parent, bool isTree = true)
    // {
    //     // Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
    //     Vector3 randomPosition = parent.position + Random.insideUnitSphere * radius;
    //     randomPosition.y = 0;
    //     return PlaceProp(randomPosition, Quaternion.identity, parent, isTree);
    // }

    // private GameObject PlaceRandomProp(bool isTree = true)
    // {
    //     Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
    //     randomPosition.y = 0;
    //     return PlaceProp(randomPosition, Quaternion.identity, isTree);
    // }

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

        GameObject tree = Instantiate(TreePrefab, position + parentCluster.position, rotation, parentCluster);

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
        GameObject bush = Instantiate(BushPrefab, position + parentCluster.position, rotation, parentCluster);

        Vector3 scale = new Vector3(width, height, width);
        bush.transform.localScale = Vector3.Scale(scale, bush.transform.localScale);

        if (leafMaterialOverride)
        {
            bush.transform.Find("Sphere").GetComponent<MeshRenderer>().material = leafMaterialOverride;

        }

        return bush;

    }

}
