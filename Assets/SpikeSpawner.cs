using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class SpikeSpawner : MonoBehaviour
{
    public GameObject spikePrefab;
    public BoxCollider2D wallScale;
    float spawnX = 8.0859f;
    public static int numberOfSpikes = 30;
    public PlayerMovement Player;
    float spikeIncrement = 0.12f;
    int[] previousSpike;
    int spawnTile;

    void Start()
    {
        SpawnSpikes();
    }

    public void SpawnSpikes()
    {
        previousSpike = new int[numberOfSpikes];
        for (int u = 0; u < numberOfSpikes; u++)
        {
            previousSpike[u] = 0;
        }

        for (int i = 0; i < SpikeSpawner.numberOfSpikes; i++)
        {
            Instantiate(spikePrefab, SpikeOverlap(i), Quaternion.Euler(0f, 0f, 90f));
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
    public void DestroySpike()
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Spike");

        foreach (GameObject DeathSpike in objectsToDestroy)
        {
            Destroy(DeathSpike);
        }
    }
}

