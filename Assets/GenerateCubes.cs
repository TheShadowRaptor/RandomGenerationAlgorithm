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
    public bool smoothHeightToCenter = true;
    public bool smoothHeightToBorder = true;
    public bool smoothHeightToInnerComplex = true;

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

                    // Will fill space or leave it blank
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
                        for (int y = 0; y <= GradualHeightSmoothing(currentBlockHeight, x, z); y++)
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

    float GradualHeightSmoothing(float currentBlockHeight, int x, int z)
    {
        int currentBlockPosX = x;
        int currentBlockPosZ = z;

        int blockMiddlePosX = blockMaxPosX / 2;
        int blockMiddlePosZ = blockMaxPosZ / 2;

        if (smoothHeightToCenter)
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

        if (smoothHeightToBorder)
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

        if (smoothHeightToInnerComplex)
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
}
