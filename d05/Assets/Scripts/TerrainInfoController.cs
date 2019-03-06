using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInfoController : MonoBehaviour
{
    public GolfController golfController;

    private int surfaceIndex = 0;
    private Terrain terrain;
    private TerrainData terrainData;
    private Vector3 terrainPos;
    void Start()
    {
        terrain = Terrain.activeTerrain;
        terrainData = terrain.terrainData;
        terrainPos = terrain.transform.position;
    }

    void Update()
    {
        surfaceIndex = GetMainTexture(transform.position);
        if (surfaceIndex == 0)
        {
            golfController.terrainForward = 1.0f;
            golfController.terrainUp = 1.0f;
            golfController.terrainIndex = 0;
        }
        else if (surfaceIndex == 1)
        {
            golfController.terrainForward = 0.7f;
            golfController.terrainUp = 0.8f;
            golfController.terrainIndex = 1;
        }
        else if (surfaceIndex == 2)
        {
            golfController.terrainForward = 1.0f;
            golfController.terrainUp = 1.0f;
            golfController.terrainIndex = 2;
        }
    }

    private float[] GetTextureMix(Vector3 WorldPos)
    {
        int mapX = (int)(((WorldPos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = (int)(((WorldPos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);

        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

        float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];

        for (int n = 0; n < cellMix.Length; n++)
        {
            cellMix[n] = splatmapData[0, 0, n];
        }
        return cellMix;
    }

    private int GetMainTexture(Vector3 WorldPos)
    {
        float[] mix = GetTextureMix(WorldPos);

        float maxMix = 0;
        int maxIndex = 0;

        for (int n = 0; n < mix.Length; n++)
        {
            if (mix[n] > maxMix)
            {
                maxIndex = n;
                maxMix = mix[n];
            }
        }
        return maxIndex;
    }
}
