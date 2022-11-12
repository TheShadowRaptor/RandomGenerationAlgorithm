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

    [Header("Customization")]
    public List<int> roadsZ = new List<int>();
    public List<int> roadsX = new List<int>();
    public int roadSize = 3;

    public bool generationStyleBigCenter = true;
    public bool generationStyleBigOuter = true;
    public bool generationStyleInnerComplex = true;

    public bool genertaing = true;

    [Header("Color")]
    public Color floorColor = Color.green;
    public Color buildingColor = Color.white;
    public Color roadColor = Color.black;

    List<int> roadSizesZ = new List<int>();
    List<int> roadSizesX = new List<int>();

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
            RandomizeRoads();
            for (int x = 0; x <= blockMaxPosX; x++)
            {
                for (int z = 0; z <= blockMaxPosZ; z++)
                {
                    if (RoadGenerationX(x) == false)
                    {
                        if (RoadGenerationZ(z) == false)
                        {
                            emptySpaceAmount = Random.Range(0, emptySpaceWeight);

                            Vector3 localPosition = new Vector3(x, 0, z);
                            int currentBlockHeight = Random.Range(blockMinPosY, blockMaxPosY);

                            // Will fill space or leave it blank
                            if (OnMapBorder(x, 0, z) == false)
                            {
                                for (int y = 0; y <= GenerationStyle(currentBlockHeight, x, z); y++)
                                {
                                    CreateFullSpace(x, y, z, buildingColor);
                                }
                            }

                            else
                            {                              
                                CreateEmptySpace(x, 0, z, floorColor);                               
                            }
                        }
                        else
                        {
                            if (OnMapBorder(x, 0, z) == false)
                            {
                                CreateFullSpace(x, 0, z, roadColor);
                            }
                            else
                            {
                                CreateEmptySpace(x, 0, z, floorColor);
                            }
                        }
                    }
                    else
                    {
                        if (OnMapBorder(x, 0, z) == false)
                        {
                            CreateFullSpace(x, 0, z, roadColor);
                        }
                        else
                        {
                            CreateEmptySpace(x, 0, z, floorColor);
                        }
                    }
                }               
            }
            genertaing = false;
        }
    }

    float GenerationStyle(float currentBlockHeight, int x, int z)
    {
        int currentBlockPosX = x;
        int currentBlockPosZ = z;

        int blockMiddlePosX = blockMaxPosX / 2;
        int blockMiddlePosZ = blockMaxPosZ / 2;

        if (generationStyleBigCenter)
        {

            if (currentBlockPosX < blockMiddlePosX / 3 || currentBlockPosZ < blockMiddlePosZ / 3 ||
                currentBlockPosX > blockMiddlePosX * 3.5f / 2 || currentBlockPosZ > blockMiddlePosZ * 3.5f / 2)
            {
                currentBlockHeight /= 3;
            }

            else if (currentBlockPosX < blockMiddlePosX / 2 || currentBlockPosZ < blockMiddlePosZ / 2 ||
                currentBlockPosX > blockMiddlePosX * 3 / 2 || currentBlockPosZ > blockMiddlePosZ * 3 / 2)
            {
                currentBlockHeight /= 2;
            }

            else if (currentBlockPosX < blockMiddlePosX / 1.5f || currentBlockPosZ < blockMiddlePosZ / 1.5f ||
               currentBlockPosX > blockMiddlePosX * 2.5f / 2 || currentBlockPosZ > blockMiddlePosZ * 2.5f / 2)
            {
                currentBlockHeight /= 1.5f;
            }
        }

        if (generationStyleBigOuter)
        {
            if (currentBlockPosX < blockMiddlePosX / 3 || currentBlockPosZ < blockMiddlePosZ / 3 ||
                currentBlockPosX > blockMiddlePosX * 3.5f / 2 || currentBlockPosZ > blockMiddlePosZ * 3.5f / 2)
            {
                return currentBlockHeight;
            }

            else if (currentBlockPosX < blockMiddlePosX / 2 || currentBlockPosZ < blockMiddlePosZ / 2 ||
                currentBlockPosX > blockMiddlePosX * 3 / 2 || currentBlockPosZ > blockMiddlePosZ * 3 / 2)
            {
                currentBlockHeight /= 1.5f;
            }

            else if (currentBlockPosX < blockMiddlePosX / 1.1f || currentBlockPosZ < blockMiddlePosZ / 1.1f ||
               currentBlockPosX > blockMiddlePosX * 2.5f / 2 || currentBlockPosZ > blockMiddlePosZ * 2.5f / 2)
            {
                currentBlockHeight /= 4;
            }
            else if (currentBlockPosX > blockMiddlePosX / 1.1f || currentBlockPosZ > blockMiddlePosZ / 1.1f ||
               currentBlockPosX < blockMiddlePosX * 2.5f / 2 || currentBlockPosZ < blockMiddlePosZ * 2.5f / 2)
            {
                currentBlockHeight /= 4;
            }
        }

        if (generationStyleInnerComplex)
        {
            if (currentBlockPosX < blockMiddlePosX / 3 || currentBlockPosZ < blockMiddlePosZ / 3 ||
               currentBlockPosX > blockMiddlePosX * 3.5f / 2 || currentBlockPosZ > blockMiddlePosZ * 3.5f / 2)
            {
                return currentBlockHeight;
            }

            else if (currentBlockPosX < blockMiddlePosX / 2 || currentBlockPosZ < blockMiddlePosZ / 2 ||
                currentBlockPosX > blockMiddlePosX * 3 / 2 || currentBlockPosZ > blockMiddlePosZ * 3 / 2)
            {
                currentBlockHeight /= 0.5f;
            }

            else if (currentBlockPosX < blockMiddlePosX / 1.5f || currentBlockPosZ < blockMiddlePosZ / 1.5f ||
               currentBlockPosX > blockMiddlePosX * 2.5f / 2 || currentBlockPosZ > blockMiddlePosZ * 2.5f / 2)
            {
                currentBlockHeight /= 3;
            }
            else if (currentBlockPosX > blockMiddlePosX / 1.5f || currentBlockPosZ > blockMiddlePosZ / 1.5f ||
               currentBlockPosX < blockMiddlePosX * 2.5f / 2 || currentBlockPosZ < blockMiddlePosZ * 2.5f / 2)
            {
                currentBlockHeight /= 3;
            }
        }
        return currentBlockHeight;
    }
    bool OnMapBorder(int x, int y, int z)
    {
        if (emptySpaceAmount > 0 
        || x < mapBorderSize || z < mapBorderSize 
        ||  x > blockMaxPosX - mapBorderSize || z > blockMaxPosZ - mapBorderSize)
        {
            return true;
        }
        return false;
    }
    bool RoadGenerationX(float currentX)
    {
        for (int i = 0; i < roadSizesX.Count; i++)
        {
            if (currentX == roadSizesX[i])
            {
                return true;
            }
        }
        return false;
    }
    bool RoadGenerationZ(float currentZ)
    {
        for (int i = 0; i < roadSizesZ.Count; i++)
        {
            if (currentZ == roadSizesZ[i])
            {
                return true;
            }
        }
        return false;
    }
    void RandomizeRoads()
    {
        // RoadsX generation
        for (int i = 0; i < roadsX.Count; i++)
        {
            roadsX[i] = Random.Range(0, blockMaxPosX);

            for (int b = 0; b < roadsX.Count; b++)
            {
                if (roadsX[i] == roadsX[b])
                {
                    roadsX[i] = Random.Range(0, blockMaxPosX);
                }
            }

            for (int b = 0; b < roadSize; b++)
            {
                roadSizesX.Add(roadsX[i] += 1);

            }
        }

        // Roads Z generation
        for (int i = 0; i < roadsZ.Count; i++)
        {
            roadsZ[i] = Random.Range(0, blockMaxPosZ);

            for (int b = 0; b < roadsZ.Count; b++)
            {
                if (roadsZ[i] == roadsZ[b])
                {
                    roadsZ[i] = Random.Range(0, blockMaxPosZ);
                }
            }

            for (int b = 0; b < roadSize; b++)
            {
                roadSizesZ.Add(roadsZ[i] += 1);

            }
        }
    }
    void CreateEmptySpace(int x, int y, int z, Color color)
    {
        // empty space
        Vector3 localPosition = new Vector3(x, 0, z);
        Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Cube.GetComponent<Renderer>().material.color = color;
        Cube.transform.position = localPosition;
    }
    void CreateFullSpace(int x, int y, int z, Color color)
    {
        // full space
        Vector3 localPosition = new Vector3(x, y, z);
        Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Cube.GetComponent<Renderer>().material.color = color;
        Cube.transform.position = localPosition;
        if (y == 0 && Cube.GetComponent<Renderer>().material.color != roadColor)
        {
            Cube.GetComponent<Renderer>().material.color = floorColor;
        }
    }
}
