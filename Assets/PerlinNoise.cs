using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 localPosition;
        //localPosition.y = PerlinNoise2D(Time.time * frequency, 0.0f) * amplitude;
    }

    public float PerlinNoise2D(float x, float y)
    {
        return (Mathf.PerlinNoise(x, y) * 2.0f) - 1.0f;
    }
}
