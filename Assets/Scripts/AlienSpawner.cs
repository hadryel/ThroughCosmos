using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public GameObject[] AlienPrefabs;
    public Transform[] SpawnPoints;
    public int MaximumAliens = 4;
    public int CurrentAliens = 0;

    public float SpawnDelay = 5f;
    float LastSpawn;

    void Start()
    {
        LastSpawn = Time.time;

        SpawnAlien();
    }

    void SpawnAlien()
    {
        int enemyIndex = LevelManager.GetRandomEnemy();

        if (enemyIndex == -1)
            return;

        Transform destiny = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        GameObject alien = GameObject.Instantiate(AlienPrefabs[enemyIndex], destiny.position, Quaternion.identity, null);
        alien.GetComponent<EnemyController>().Home = this;
        CurrentAliens++;

        LastSpawn = Time.time;
    }

    void Update()
    {
        if(Time.time - LastSpawn >= SpawnDelay)
        {
            SpawnAlien();
        }
    }

    public void MemberKilled(int type)
    {
        CurrentAliens--;
        LevelManager.AddRandomEnemy(type);
    }
}
