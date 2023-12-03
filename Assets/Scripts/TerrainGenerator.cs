using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth;

    public int width;
    public int height;

    public float scale;


    //randommize the terrain generation

    public float offSetX;
    public float offSetY;


    private void Start()
    {
        Debug.Log("Terrain Start");
        //Randomizes terrain "seed"
        offSetX = Random.Range(0f, 9999f);
        offSetY = Random.Range(0f, 9999f);
        
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
        //heights.GetLength(0);
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
