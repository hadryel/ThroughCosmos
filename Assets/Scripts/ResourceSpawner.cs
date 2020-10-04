using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public int Index;
    float Width = 16;
    float SpawnAmount;
    float MinWidth = -8;

    int CurrentSpawnIndex = 0;

    int AlienSpawnLimit;
    int CurrentAlienSpawn = 0;

    void Awake()
    {
        SetupLevelParams();

        float offset = 0;
        if (SpawnAmount % 0 == 0)// Even 
        {
            offset = Width / SpawnAmount + SpawnAmount / 2;
        }
        else
        {
            offset = Width / SpawnAmount; // 32/5 = 6.4
        }

        float firstPosition = MinWidth + offset; // -16 + 6.4 = -9.6

        for (int i = 0; i < SpawnAmount; i++)
        {
            Vector3 position = new Vector3(firstPosition + i * offset - offset / 2, Random.Range(-3f, 3f), 0);
            var go = GameObject.Instantiate(GetResourceToSpawn());
            go.transform.localPosition = transform.position + position;
            float rotationAngle = (Random.Range(0f, 1f) >= 0.5f) ? 180f : 0f;
            go.transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
            go.transform.parent = transform;
        }
    }

    void SetupLevelParams()
    {
        LevelManager lm = GetComponentInParent<LevelManager>();

        if (lm.CurrentLevel <= 2)
        {
            SpawnAmount = Random.Range(3, 4);
        }
        else if (lm.CurrentLevel <= 4)
        {
            SpawnAmount = Random.Range(3, 6);
        }
        else // (lm.CurrentLevel <= 6)
        {
            SpawnAmount = Random.Range(4, 7);
        }

        AlienSpawnLimit = lm.CurrentLevel;
    }

    public GameObject GetResourceToSpawn()
    {
        ResourceType type;
        if (SafeChunk() && CurrentSpawnIndex <= 2)
        {
            type = (ResourceType)CurrentSpawnIndex;
        }
        else
        {
            if (CurrentAlienSpawn > AlienSpawnLimit)
            {
                type = (ResourceType)Random.Range(0, 2);
            }
            else
            {
                type = (ResourceType)Random.Range(0, 3);
            }
        }

        CurrentSpawnIndex++;
        ChunkSpawner cs = GetComponentInParent<ChunkSpawner>();

        switch (type)
        {
            case ResourceType.Stone:
                return cs.Stones[Random.Range(0, cs.Stones.Length)];
            case ResourceType.Wood:
                return cs.Wood[Random.Range(0, cs.Wood.Length)];
            case ResourceType.Alien:
                CurrentAlienSpawn++;
                return cs.Aliens[Random.Range(0, cs.Aliens.Length)];
        }

        Debug.Log("Retornando nulo! " + type);
        return null;
    }

    bool SafeChunk()
    {
        LevelManager lm = GetComponentInParent<LevelManager>();

        return lm.SafeChunkIndexes.Contains(Index);
    }
    void Update()
    {

    }
}
