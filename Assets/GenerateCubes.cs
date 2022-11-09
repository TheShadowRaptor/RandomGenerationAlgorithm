using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCubes : MonoBehaviour
{
    [Header("MapSize")]
    public int blockMaxPosX = 10;
    public int blockMaxPosZ = 10;
    public int blockMaxPosY = 4;
    public int blockMinPosY = 0;

    [Header("Limitations")]
    public int mapBorderSize = 3;

    [Header("Weights")]
    public int emptySpaceWeight = 2;
    private int emptySpaceAmount;

    [Header("PerlinNoise")]
    [Range(0, 10)][SerializeField] float amplitude = 5.0f;
    [Range(0, 10)][SerializeField] float frequency = 5.0f;

    public PerlinNoise perlinNoise;
    public bool genertaing = true;

    GameObject Cube;
    // Start is called before the first frame update
    void Start()
    {
        MapGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MapGeneration()
    {
        if (genertaing)
        {
            for (int x = 0; x <= blockMaxPosX; x++)
            {
                for (int z = 0; z <= blockMaxPosZ; z++)
                {
                    emptySpaceAmount = Random.Range(0, emptySpaceWeight);

                    Vector3 localPosition = new Vector3(x, 0, z);
                    int currentBlockHeight = Random.Range(blockMinPosY, blockMaxPosY);
                    if (emptySpaceAmount > 0 || x < mapBorderSize || z < mapBorderSize ||
                    x > blockMaxPosX - mapBorderSize || z > blockMaxPosZ - mapBorderSize)
                    {
                        Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Cube.transform.position = localPosition;
                        Debug.Log("EmptySpace");
                        Debug.Log(emptySpaceAmount);
                    }

                    else
                    {
                        for (int y = 0; y <= currentBlockHeight; y++)
                        {
                            localPosition.y = y;
                            Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            Cube.transform.position = localPosition;
                            Debug.Log("NotEmptySpace");
                            Debug.Log(emptySpaceAmount);
                        }
                    }
                }
            }
            genertaing = false;
        }
    }

    void SmoothHeight()
    {

    }
}
