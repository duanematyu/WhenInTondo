using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    float timeBetween;
    public float startTimeBetween;

    EnemyController enemyController;

    PlayerHealth playerHealth;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyController = GetComponentInChildren<EnemyController>();
        timeBetween = startTimeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHealth.isDead)
        {
            if (timeBetween <= 0 && enemyController.isInRange)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                timeBetween = startTimeBetween;
            }
            else
            {
                timeBetween -= Time.deltaTime;
            }
        }        
    }
}
