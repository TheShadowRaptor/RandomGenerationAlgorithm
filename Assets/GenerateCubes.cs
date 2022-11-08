using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCubes : MonoBehaviour
{
    [Header("MapSize")]
    public int blockGenSizeHorizontal = 10;
    public int blockGenSizeVertical = 10;

    [Header("PerlinNoise")]
    [Range(0, 10)][SerializeField] float amplitude = 5.0f;
    [Range(0, 10)][SerializeField] float frequency = 5.0f;

    public PerlinNoise perlinNoise;
    GameObject Cube;
    public bool genertaing = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (genertaing)
        {
            for (int x = 0; x <= blockGenSizeHorizontal; x++)
            {
                for (int z = 0; z <= blockGenSizeVertical; z++)
                {
                    Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Vector3 localPosition = new Vector3(x, 0, z);
                    localPosition.y = perlinNoise.PerlinNoise2D(Time.time * frequency, 0.0f) * amplitude;
                    Cube.transform.position = localPosition;
                }
            }
            genertaing = false;
        }       
    }
}
