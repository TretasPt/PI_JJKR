using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{

    public static Trees Instance { get; private set; }

    public GameObject objectToSpawn;

    public Vector3 origin = Vector3.zero;
    public float radius = 10;



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

        PlaceTree(0, 0);


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
    {

    }

    private void GenerateTreeCluster()
    {

    }

    private void PlaceTree(int X, int Y)
    {
        Debug.Log(objectToSpawn);
        GameObject Tree = Instantiate(objectToSpawn);

        // GameObject Tree = Instantiate(objectToSpawn, transform.position, Quaternion.identity);

        // Finds a position in a sphere with a radius of 10 units.
        // Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
        // Instantiate(objectToSpawn, randomPosition, Quaternion.identity);


    }

}
