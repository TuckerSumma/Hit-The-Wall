using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LSpikeSpawner : MonoBehaviour
{
    public GameObject spikePrefab;
    public BoxCollider2D wallScale;
    float spawnX = -8.0859f;
    float spikeIncrement = 0.12f;
    int[] previousSpike;
    int spawnTile;


    public void SpawnSpikes()
    {
        previousSpike = new int[SpikeSpawner.numberOfSpikes];
        for (int u = 0; u < SpikeSpawner.numberOfSpikes; u++) 
        {
            previousSpike[u] = 0;
        }

        for (int i = 0; i < SpikeSpawner.numberOfSpikes; i++)
        {
            Instantiate(spikePrefab, SpikeOverlap(i), Quaternion.Euler(0f, 0f, -90f));
            previousSpike[i] = spawnTile;
        }
    }

    public Vector2 SpikeOverlap(int index)
    {
        
        float wallTop = wallScale.bounds.max.y - 0.80f;
        spawnTile = UnityEngine.Random.Range(1, 36);    
        Vector2 spawnPosition = new Vector2(spawnX, (wallTop - (spawnTile * 2 * spikeIncrement)));
        for (int i = 0; i < index; i++)
        {
            if (spawnTile == previousSpike[i])
            {
                return SpikeOverlap(index);
            }
        }
        return spawnPosition;
    }
    public void RandomGroups() 
    {
        if (PlayerMovement.score >= 20 && PlayerMovement.score < 30)
        {
            int group1Range = UnityEngine.Random.Range(8, 12);
            int group2Range = UnityEngine.Random.Range(8, 12);
            int group1Start = UnityEngine.Random.Range(3, 33);
            int group2Start = UnityEngine.Random.Range(3, 33);
        }
    }
}

