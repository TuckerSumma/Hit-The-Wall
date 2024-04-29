using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LSpikeSpawner : MonoBehaviour
{
    public GameObject spikePrefab;
    public Transform wallTransform;
    float spawnX = -7.6f;
    GameObject[] previousSpike;


    public void SpawnSpikes()
    {
        float wallTop = (wallTransform.position.y + wallTransform.localScale.y / 2) - 0.7f * 2f;
        float wallBottom = (wallTransform.position.y - wallTransform.localScale.y / 2) + 0.7f * 2f;

        previousSpike = new GameObject[SpikeSpawner.numberOfSpikes];
        for (int i = 0; i < SpikeSpawner.numberOfSpikes; i++)
        {
            GameObject newSpike = Instantiate(spikePrefab, SpikeOverlap(wallTop, wallBottom, i), Quaternion.Euler(0f, 0f, -90f));
            previousSpike[i] = newSpike;
        }
    }

    public Vector2 SpikeOverlap(float WallTop, float WallBottom, int index)
    {
        Vector2 spawnPosition = new Vector2(spawnX, Random.Range(WallBottom, WallTop));
        for (int i = 0; i < index; i++)
        {
            {
                Vector2 prevPos = previousSpike[i].transform.position;
                if (Vector2.Distance(prevPos, spawnPosition) < 0.8)
                {
                    return SpikeOverlap(WallBottom, WallTop, index);
                }
            }
        }
        return spawnPosition;
    }
}
