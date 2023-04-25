using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : EnemyStats
{
    public float speed;
    Rigidbody2D rb;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, 0).normalized * speed;
    }
    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            this.gameObject.GetComponent<EnemyBullet>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Instantiate(transform.position, Quaternion.identity);
        if (player != null)
        {
            if (collision.tag == "Player" && !playerHealth.isInvulnerable && !playerMovement.isInvulnerable)
            {
                playerHealth.TakeDamage(baseAttack);
                Destroy(gameObject);
            }

            if (collision.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }      
    }
}
