using UnityEngine;

public class PerlinNoise : MonoBehaviour    
{

    public int width;
    public int height;

    public float scale;

    public float offsetX;
    public float offsetY;

    private void Start()        //It allows for each scene that's created to be different from the one made before
    {
        offsetX = Random.Range(0f, 999f); 
        offsetY = Random.Range(0f, 999f);
    }

    private void Update()
    {
    }


}

