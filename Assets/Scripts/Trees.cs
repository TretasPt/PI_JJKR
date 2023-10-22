using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{

    public static Trees Instance { get; private set; }


    public GameObject objectToSpawn;

    // public GameObject ObjectParent;

    public Vector3 origin = Vector3.zero;
    public float radius = 100;



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

        int numberOfClusters = GenerateNumberOfTreeCluesters(3);

        // PlaceTree(0, 0);
        PlaceTree();
        // PlaceRandomTree();
        // PlaceRandomTree();
        // PlaceRandomTree();

        ////Seems playable up to 20000 trees.
        // for (int i = 0; i < 10000; i++)
        // {
        // PlaceRandomTree();
        // }


        // // Initialise round duration
        // roundDuration = GenerateRoundDuration(lambda);

        // // Initialise number of crates to spawn
        // numberOfCratesToSpawn = GenerateRandomNumberOfCrates(n, p);

        // // Display game parameters to the console
        // Debug.Log($"Round duration: {roundDuration}");
        // Debug.Log($"Number of crates to spawn: {numberOfCratesToSpawn}");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int GenerateNumberOfTreeCluesters(int max)
    {
        //TODO Make random
        return max;
    }

    private void GenerateTreeClusters()
    //TODO
    {

    }

    private void GenerateTreeCluster()
    //TODO
    {

    }

    private void PlaceTree(Vector3 position, Quaternion rotation)
    {
        // GameObject Tree = Instantiate(objectToSpawn);
        // GameObject Tree = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        GameObject Tree = Instantiate(objectToSpawn, position, rotation, transform);

        // Finds a position in a sphere with a radius of 10 units.
        // Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
        // Instantiate(objectToSpawn, randomPosition, Quaternion.identity);


    }

    private void PlaceTree()
    {
        PlaceTree(transform.position, Quaternion.identity);
    }

    private void PlaceRandomTree()
    {
        // Finds a position in a sphere with a radius of 10 units.
        // Vector3 rand = origin + Random.onUnitSphere * radius;
        // rand.y=0;
        // PlaceTree(rand, Quaternion.identity);
        Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
        randomPosition.y=0;
        PlaceTree(randomPosition, Quaternion.identity);


    }

}
