using System;
using Unity.VisualScripting;

// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Singleton class <c>Trees</c> represents the forest and it's generation.
/// <para>
/// The forest is comprised of clusters of Trees and Bushes.
/// </para>
/// </summary>
public class Trees : MonoBehaviour
{

    /// <value>
    /// Object where to spawn the props at.
    /// <para>
    /// Only it's dimentions are used.
    /// </para>
    /// </value>
    public GameObject Floor;

    /// <value>
    /// (0,0,0)
    /// Origin of the map.
    /// </value>
    public static readonly Vector3 origin = Vector3.zero;

    /// <value>
    /// Maximum amount of props to be generated.
    /// 
    /// <para>
    /// This prop may be either a Tree or a Bush prop.
    /// </para>
    /// <para>
    /// Default value. Likely overwriden in Inspector.
    /// </para>
    /// </value>
    public int MAX_PROPS = 5000;

    /// <value>
    /// Minimum amount of environment clusters to generate.
    /// <para>
    /// Default value. Likely overwriden in Inspector.
    /// </para>
    /// </value>
    public int MIN_CLUSTERS = 10;

    /// <value>
    /// Maximum amount of environment clusters to generate.
    /// <para>
    /// Default value. Likely overwriden in Inspector.
    /// </para>
    /// </value>
    public int MAX_CLUSTERS = 10;

    /// <summary>
    /// Ratio of Trees to Bushes.
    /// <para>
    /// Must be between 0(no trees) and 1(only trees).
    /// </para>
    /// <para>
    /// Default value. Likely overwriden in Inspector.
    /// </para>
    /// </summary>
    public float TREE_RATIO = 0.7f;

    private const float TreeScale = 3;
    private const float BushScale = 3;
    private const int numBushesInResources = 5; //Because there are 5 types of bushes in ./Assets/Resources folder
    private const int numTreesInResources = 10; //Because there are 10 types of trees in ./Assets/Resources folder
    private Terrain terrain;




    /// <summary>
    /// Start runs once at the Start(after awake).
    /// <para>
    /// Generates the forest itself. The entire script is meant to run here.
    /// </para>
    /// </summary>
    void Start()
    {
        Debug.Log("Start.");

        terrain = Floor.GetComponent<Terrain>();

        int numberOfClusters = GenerateNumberOfForestCluesters(MIN_CLUSTERS, MAX_CLUSTERS);
        GenerateForestClusters(numberOfClusters);
    }

    /// <summary>
    /// Update runs once per frame.
    /// <para>
    /// May be used in the future to destroy props or affect them in other ways.
    /// </para>
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// Generates <c>numberOfClusters</c> clusters.
    /// <para>
    /// Generates clusters from <c>numberOfClusters</c> to 1.
    /// </para>
    /// <para>
    /// Takes care of equally dividing the props(bushes or trees) per cluster and populating the clusters.
    /// </para>
    /// </summary>
    /// <param name="numberOfClusters">Amount of clusters to be generated.</param>
    private void GenerateForestClusters(int numberOfClusters)
    {

        int propsToPlace = MAX_PROPS;//Remaining props not placed yet.

        for (int i = numberOfClusters; i > 0; i--)
        {

            GameObject cluster = new GameObject("Cluster-" + i);
            cluster.transform.parent = transform;//Set each cluster as a child of Trees

            int propsInCurrentCluster = propsToPlace / i;

            PopulateCluster(propsInCurrentCluster, cluster, TREE_RATIO);

            propsToPlace -= propsInCurrentCluster;


        }
    }

    /// <summary>
    /// Populates a cluster composed of a central tree and <c>numberOfTrees</c> trees and bushes around it.
    /// <para>
    /// Generates all the props that compose the cluster.
    /// </para>
    /// <para>
    /// If the generated trees end up with too many generations(more than 5 to 10 should be big bumbers) it may mean the cluster is overpopulated with Trees.
    /// Solve it by decreasing the <c>treeRatio</c> or the <c>numberOfProps</c>.
    /// </para>
    /// </summary>
    /// <param name="numberOfProps">Amount of forest props to generate(Trees and Bushes).</param>
    /// <param name="clusterParent">Parent "forest" or "cluster".</param>
    /// <param name="treeRatio">It generates <c>treeRatio</c> trees and 1-<c>treeRatio</c> bushes.</param>
    private void PopulateCluster(int numberOfProps, GameObject clusterParent, float treeRatio = 0.7f)
    {

        GameObject centralTree = placeMainTree(clusterParent);
        centralTree.name = "CentralTree";

        int generation = 0;
        int placedProps = 1;
        while (placedProps < numberOfProps)
        {
            int placeThisCicle = numberOfProps - placedProps;
            int lastProp = (int)(placeThisCicle * treeRatio);
            for (int i = 0; i != placeThisCicle; i++)
            {
                double mean = RandomVariables.Uniform(5f,50f);
                double sDeviation = RandomVariables.Uniform(1f,50f);
                Vector2 propLocation = GeneratePropLocation(mean,sDeviation);

                GameObject currentObject = PlaceProp(PolarToCartesian(propLocation), Quaternion.identity, clusterParent.transform, i < lastProp);

                if (currentObject != null)
                {
                    placedProps++;
                    currentObject.name += " - gen " + generation + " - " + i;
                }

            }
            generation++;
        }

    }

    /// <summary>
    /// Generates a new Tree or Bush.
    /// </summary>
    /// <param name="position">Relative position to the parentCluster center.</param>
    /// <param name="rotation">Prop orientation. May be used to set an inclination.</param>
    /// <param name="parent">Forest cluster object. The parent of this Prop.</param>
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
        Vector2 polarCoords = new Vector2(angle, radious);
        return PolarToCartesian(polarCoords);
    }

    /// <summary>
    /// Translates a given 2d polar coordinate into a Vector3 cartesian coordinate with height 0.
    /// </summary>
    /// <param name="polarCoords">Polar coordinates with x representing the angle and y representing the radious.</param>
    /// <returns></returns>
    private Vector3 PolarToCartesian(Vector2 polarCoords)
    {
        return new Vector3(Mathf.Cos(polarCoords.x) * polarCoords.y, 0, Mathf.Sin(polarCoords.x) * polarCoords.y);
    }

    /// <summary>
    /// Generate a Tree prop.
    /// </summary>
    /// <param name="position">Relative position to the parentCluster center.</param>
    /// <param name="rotation">Tree orientation. May be used to set an inclination.</param>
    /// <param name="parentCluster">Forest cluster object. The parent of this Tree</param>
    /// <param name="height">Tree height multiplier.</param>
    /// <param name="width">Tree width multiplier.</param>
    /// <param name="leafMaterialOverride">If set will override the leaf color.</param>
    /// <param name="allowOutOfBounds">Defines if the tree may skip out of bounds checks.</param>
    /// <returns>A Tree GameObject or null if it fails.</returns>
    private GameObject GenerateTree(Vector3 position, Quaternion rotation, Transform parentCluster, float height = 1, float width = 1, Material leafMaterialOverride = null, bool allowOutOfBounds = false)
    {

        position += parentCluster.position;

        //Check if the tree is inside the map.
        if (!allowOutOfBounds && !isInsideMap(position))
            return null;

        //Check for tree colisions.
        foreach (var collider in Physics.OverlapCapsule(position, position + new Vector3(0, 10, 0), 1))
        {
            if (collider.gameObject.CompareTag("Tree"))
            {
                return null;
            }
        }

        //Generate newPosition based on terrain height
        float newY = terrain.SampleHeight(new Vector3(position.x, 0, position.z));
        Vector3 newPosition = new Vector3(position.x, newY, position.z);

        GameObject randomTree = choseRandomTree();
        GameObject tree = Instantiate(randomTree, newPosition, rotation, parentCluster);
        // Debug.Log("New Position: " + newPosition + " NewY: " + newY);
        tree.transform.localScale = new Vector3(TreeScale, TreeScale, TreeScale);
        tree.name = "Tree";
        tree.tag="Tree";

        return tree;
    }


    /// <summary>
    /// Generate a Bush prop.
    /// </summary>
    /// <param name="position">Relative position to the parentCluster center.</param>
    /// <param name="rotation">Bush orientation. May be used to set an inclination. Has no use with default material.</param>
    /// <param name="parentCluster">Forest cluster object. The parent of this Bush</param>
    /// <param name="height">Bush height multiplier.</param>
    /// <param name="width">Bush width multiplier.</param>
    /// <param name="leafMaterialOverride">If set will override the leaf color.</param>
    /// <param name="allowOutOfBounds">Defines if the bush may skip out of bounds checks.</param>
    /// <returns>A Bush GameObject or null if it fails.</returns>
    private GameObject GenerateBush(Vector3 position, Quaternion rotation, Transform parentCluster, float height = 1, float width = 1, Material leafMaterialOverride = null, bool allowOutOfBounds = false)
    {

        position += parentCluster.position;

        //Out of bounds check.
        if (!allowOutOfBounds && !isInsideMap(position))
            return null;

        //Generate newPosition based on terrain height
        float newY = terrain.SampleHeight(new Vector3(position.x, 0, position.z));
        Vector3 newPosition = new Vector3(position.x, newY, position.z);

        GameObject randomBush = choseRandomBush();
        GameObject bush = Instantiate(randomBush, newPosition, rotation, parentCluster);
        bush.transform.localScale = new Vector3(BushScale, BushScale, BushScale);
        bush.name = "Bush";
        bush.tag = "Bush";

        return bush;
    }

    /// <summary>
    /// Checks if a given position is inside the bounds of the Floor's transform.
    /// 
    /// <para>
    /// ATENTION! Update this if the floor is updated.
    /// </para>
    /// </summary>
    /// <param name="position">Position to check.</param>
    /// <returns></returns>
    private bool isInsideMap(Vector3 position)
    {
        Vector3 floorTransform = Floor.transform.lossyScale;//May be better to use localScale
        return position.x < floorTransform.x / 2 && position.z < floorTransform.z / 2 && position.x > -floorTransform.x / 2 && position.z > -floorTransform.z / 2;
    }

    /// <summary>
    /// Places the main tree, handling the edge cases in which it is not generated.
    /// </summary>
    /// <param name="clusterParent">The cluster to use as the parent for the tree.</param>
    /// <returns>A Tree GameObject.</returns>
    private GameObject placeMainTree(GameObject clusterParent)
    {
        int maintTreeWidthMultiplier = 3;
        int maintTreeHeightMultiplier = 2;

        positionCluster(clusterParent);

        GameObject generatedTree = null;
        //While tree not successefuly generated.
        while (generatedTree == null)
        {
            //If a relevant colision is detected.
            foreach (var collider in Physics.OverlapCapsule(clusterParent.transform.position, clusterParent.transform.position + new Vector3(0, maintTreeHeightMultiplier, 0), maintTreeWidthMultiplier))
            {
                if (collider.gameObject.CompareTag("Tree"))
                {
                    positionCluster(clusterParent);
                    break;
                }
            }
            generatedTree = GenerateTree(origin, Quaternion.identity, clusterParent.transform, maintTreeHeightMultiplier, maintTreeWidthMultiplier);
        }
        return generatedTree;
    }






    /// <summary>
    /// Generate number of clusters to be generated.
    /// </summary>
    /// <param name="min">Minimum amount of clusters to generate.</param>
    /// <param name="max">Maximum amount of clusters to generate.</param>
    /// <returns>The ammount of clusters to generate.</returns>
    private int GenerateNumberOfForestCluesters(int min, int max)
    {
        return RandomVariables.Uniform(min, max);
    }

    /// <summary>
    /// Generates the polar coordinates of a new prop.
    /// </summary>
    /// <returns>A vector containing the angle as the x and the radious as the y.</returns>
    private static Vector2 GeneratePropLocation(double mean = 50, double sDeviation =20)
    {
        float angle = (float)RandomVariables.Uniform(0f, 2 * Mathf.PI);
        float radious = (float)RandomVariables.NormalBounded(mean, sDeviation, 0, 200);
        return new Vector2(angle, radious);
    }

    /// <summary>
    /// Reposition the given cluster to a randomly selected place.
    /// </summary>
    /// <param name="clusterParent">The cluster to be repositioned.</param>
    private void positionCluster(GameObject clusterParent)
    {
        float x = (float)RandomVariables.Uniform(-Floor.transform.localScale.x / 2, Floor.transform.localScale.x / 2);
        float z = (float)RandomVariables.Uniform(-Floor.transform.localScale.z / 2, Floor.transform.localScale.z / 2);

        clusterParent.transform.SetLocalPositionAndRotation(new Vector3(x, 0, z), Quaternion.identity);
    }
    private GameObject choseRandomTree()
    {
        return Resources.Load<GameObject>("Tree_" + RandomVariables.Uniform(0, numTreesInResources));
    }

    private GameObject choseRandomBush()
    {
        return Resources.Load<GameObject>("Bush_" + RandomVariables.Uniform(0, numBushesInResources));
    }


}


