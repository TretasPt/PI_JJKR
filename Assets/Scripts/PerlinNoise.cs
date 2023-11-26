using UnityEngine;

public class PerlinNoise : MonoBehaviour    
{

    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    private void Start()        //It allows for each scene that's created to be different from the one made before
    {
        offsetX = Random.Range(0f, 999f); 
        offsetY = Random.Range(0f, 999f);
    }

    private void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();   
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        // Gerar um mapa para a texture em Perlin noise
        for (int i = 0; i != width; i++)
        {
            for (int j = 0; j != height; j++)
            {
                Color color = CalculateColor(i, j);
                texture.SetPixel(i, j, color);
            }
        }

        texture.Apply();
        return texture;
    }


    Color CalculateColor(int x, int y)      //it will give us a Perlin Function Value at a certain coordinate
    {
        //Perlin Noise repeats at integer numbers so we are going to use decimal
        //We will do these by making x and y go from 0 to 1

        float xPerlin = (float) x / width * scale + offsetX;      //the smaller the x the closer to 0 and the bigger the x the closer to 1
        float yPerlin = (float) y / height * scale + offsetY;

        float value = Mathf.PerlinNoise(xPerlin, yPerlin);      
        return new Color (value, value, value);    
    }

}

