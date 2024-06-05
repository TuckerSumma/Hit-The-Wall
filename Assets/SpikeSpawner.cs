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
    public static int numberOfSpikes = 5;
    public PlayerMovement Player;
    float spikeIncrement = 0.12f;
    int[] previousSpike;
    int spawnTile;
    float wallTop;
    int[] emptySpike;

    void Start()
    {
        SpawnSpikes();
    }

    public void SpawnSpikes()
    {
        wallTop = wallScale.bounds.max.y - 0.80f;
        if (PlayerMovement.score % 4 == 0 && PlayerMovement.score != 0 && PlayerMovement.score <= 80)
        {
            SpikeSpawner.numberOfSpikes++;
        }
        previousSpike = new int[SpikeSpawner.numberOfSpikes];
        for (int u = 0; u < SpikeSpawner.numberOfSpikes; u++)
        {
            previousSpike[u] = 0;
        }

        if (PlayerMovement.score < 60)
        {
            RandomGroups();
        }
        else
        {
            HardMode();
        }
    }

    public Vector2 SpikeOverlap(int index)
    {

        spawnTile = UnityEngine.Random.Range(0, 35);
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
        int maxRange = 36 - SpikeSpawner.numberOfSpikes;
        int group1Range = UnityEngine.Random.Range(8, maxRange/2);
        int group2Range = maxRange - group1Range;
        int group1Start = UnityEngine.Random.Range(0, 35 - group1Range);
        int group2Start = UnityEngine.Random.Range(0, 35 - group2Range);
        if (group1Start <= group2Start && group2Start <= group1Start + group1Range -1 || group1Start <= group2Start + group2Range -1 && group2Start + group2Range -1 <= group1Start + group1Range -1 ||
            group2Start <= group1Start && group1Start <= group2Start + group2Range -1 || group2Start <= group1Start + group1Range -1 && group1Start + group1Range -1 <= group2Start + group2Range -1 || group1Start + group1Range == group2Start || group2Start + group2Range == group1Start)
        {
            Debug.Log("Restarted");
            RandomGroups();
        }
        else
        {
            Debug.Log(group1Start);
            Debug.Log(group1Range);
            Debug.Log(group2Start);
            Debug.Log(group2Range);
            emptySpike = new int[36];
            for (int i = 0; i <= 35; i++)
            {
                if (group1Range > i)
                {
                    emptySpike[group1Start+i] = 1;
                }
                if (group2Range > i)
                {
                    emptySpike[group2Start+i] = 1;
                }
            }
            for (int i = 0; i <= 35; i++)
            {
                if (emptySpike[i] != 1)
                {
                    Vector2 spawnPosition = new Vector2(spawnX, (wallTop - (i * 2 * spikeIncrement)));
                    Instantiate(spikePrefab, spawnPosition, Quaternion.Euler(0f, 0f, 90f));
                }
            }
        }
    }
    public void HardMode()
    {
        int groupRange = 32 - SpikeSpawner.numberOfSpikes;
        int groupStart = UnityEngine.Random.Range(0, 35 - groupRange);
        emptySpike = new int[36];
        for (int i = 0; i <= 35; i++)
        {
            if (groupRange > i)
            {
                emptySpike[groupStart+i] = 1;
            }
        }
        for (int i = 0; i <= 35; i++)
        {
            if (emptySpike[i] != 1)
            {
                Vector2 spawnPosition = new Vector2(spawnX, (wallTop - (i * 2 * spikeIncrement)));
                Instantiate(spikePrefab, spawnPosition, Quaternion.Euler(0f, 0f, 90f));
            }
        }
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

