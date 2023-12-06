using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth;

    public int width;
    public int height;

    public float scale;


    public float offSetX;
    public float offSetY;

    public bool realTimeUpdate = false;


    void Update()
    {
        if(realTimeUpdate){
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        }
    }

    private void Start()
    {
        Debug.Log("Terrain Start");

        GetComponent<Transform>().SetPositionAndRotation(new Vector3(-width/2,0,-height/2),Quaternion.identity);

        offSetX = (float)RandomVariables.Uniform(0f,100000f);
        offSetY = (float)RandomVariables.Uniform(0f,100000f);
        
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        terrain.gameObject.GetComponent<TerrainCollider>().enabled = true;
        Debug.Log("Terrain End");
    }
    TerrainData GenerateTerrain (TerrainData terrainData) 
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3 (width, depth, height);      //(x,y,z)
        float[,] heights = GenerateHeights();
        terrainData.SetHeights(0, 0, heights);
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for (int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++)
            {
                heights[i, j] = CalculateHeight(i, j);         //SOME PERLIN NOISE VALUE
            }    
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float) x / width * scale + offSetX;
        float yCoord = (float) y / height * scale + offSetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
