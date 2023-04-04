using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyStats
{
    public Transform firePoint;
    public GameObject bullet;
    float timeBetween;
    public float startTimeBetween;
    public float retreatDistance;
    public float speed;

    EnemyController enemyController;

    PlayerHealth playerHealth;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyController = GetComponentInChildren<EnemyController>();
        timeBetween = startTimeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Death();
        }
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
        
        if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            Debug.Log("Go away from player");
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), -speed * Time.deltaTime);
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
