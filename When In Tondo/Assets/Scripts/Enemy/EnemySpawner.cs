using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] activeMonsters;
    public List<GameObject> monsterPrefabs;
    public float spawnTime = 3f;
    public float spawnInterval = 2f;

    public int maxSpawnCount = 5;

    public Transform spawnPosition;
    private int spawnCount = 0;

    public bool canSpawn = true;


    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 1.5f);
    }
    void Update()
    {
        activeMonsters = GameObject.FindGameObjectsWithTag("Enemy");
    }




    void SpawnEnemy()
    {
        if (spawnCount < maxSpawnCount)
        {
            Instantiate(monsterPrefabs[UnityEngine.Random.Range(0, monsterPrefabs.Count)], spawnPosition.position, Quaternion.identity);
            spawnCount++;
        }
    }

    public void OnDeath()
    {
        //Invoke("SpawnEnemy", spawnTime);
        spawnCount--;
    }

}
