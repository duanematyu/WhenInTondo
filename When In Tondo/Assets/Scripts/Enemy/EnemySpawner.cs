using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnerPrefab;

    [SerializeField]
    private float spawnInterval = 3.5f;

    public float enemyCount;
    public float spawnTime;
    public bool canSpawn;

    //public BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", 0.5f);
    }

    private void Update()
    {
        if(canSpawn)
        {
            spawnTime -= Time.deltaTime;
            if(spawnTime < 0)
            {
                canSpawn = false;
            }

        }

        /*if (enemyCount == 0)
        {
            box.enabled = false;
        }*/
        
    }


    void SpawnEnemy()
    {
        if (enemyCount != 0)
        {
            GameObject newEnemy = Instantiate(spawnerPrefab, gameObject.transform.position, Quaternion.identity);
            enemyCount--;
        }

        Invoke("SpawnEnemy", spawnInterval);
    }
}
