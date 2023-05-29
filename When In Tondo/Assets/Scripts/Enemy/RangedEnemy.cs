using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyStats
{
    public Transform firePoint;
    public GameObject bullet;
    public GameObject spawner;
    float timeBetween;
    public float startTimeBetween;
    public float retreatDistance;
    public float speed;

    public Animator rangedEnemyAnim;
    public SpriteRenderer renderer;

    EnemyController enemyController;
    EnemySpawner enemySpawner;
    PlayerHealth playerHealth;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindWithTag("Spawner");
        enemySpawner = spawner.GetComponent<EnemySpawner>();
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
            rangedEnemyAnim.SetBool("isWalking", false);
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

        if (player.position.x < transform.position.x)
        {
            renderer.flipX = true;
            if (player.localScale.x == -1)
            {
                TurnAround();
            }
        }

        if (player.position.x > transform.position.x)
        {
            renderer.flipX = false;
            if (player.localScale.x == 1)
            {
                TurnAround();
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        if (hit.collider == null)
        {
            Debug.Log("I'm about to fall off!");
            return;
        }

        if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            rangedEnemyAnim.SetBool("isWalking", true);
            Debug.Log("Go away from player");
            transform.position = Vector2.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), -speed * Time.deltaTime);
        }
    }
    void TurnAround()
    {
        float vecToTarget = player.transform.position.x - transform.position.x;
        if (player != null)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(vecToTarget), transform.localScale.y, transform.localScale.z);
        }
    }

    private void Death()
    {
        enemySpawner.OnDeath();
        Destroy(gameObject);
    }
}
