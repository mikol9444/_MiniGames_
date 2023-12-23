    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _08PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public int maxPlatforms = 10;
    public float minY = 1f;
    public float maxY = 5f;
    public float minX = -5f;
    public float maxX = 5f;
    public float platformSpacing = 2f;

    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        GenerateInitialPlatforms();
    }

    void Update()
    {
        GeneratePlatformsBasedOnPlayerPosition();
    }

    void GenerateInitialPlatforms()
    {
        for (int i = 0; i < maxPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void GeneratePlatformsBasedOnPlayerPosition()
    {
        // Assuming the player is at position (0,0). Adjust as needed.
        float playerYPosition = transform.position.y;

        while (platforms.Count < maxPlatforms)
        {
            float newPlatformY = maxY + platformSpacing * platforms.Count;
            if (playerYPosition + 10f > newPlatformY)  // Adjust the 10f based on how far ahead you want to generate platforms.
            {
                SpawnPlatform();
            }
            else
            {
                break;
            }
        }

        RemoveOldPlatforms();
    }

    void SpawnPlatform()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), maxY + platformSpacing * platforms.Count);
        GameObject newPlatform = Instantiate(platformPrefab, randomPosition, Quaternion.identity);
        platforms.Add(newPlatform);
    }

    void RemoveOldPlatforms()
    {
        while (platforms.Count > maxPlatforms)
        {
            GameObject oldPlatform = platforms[0];
            platforms.RemoveAt(0);
            Destroy(oldPlatform);
        }
    }
}
