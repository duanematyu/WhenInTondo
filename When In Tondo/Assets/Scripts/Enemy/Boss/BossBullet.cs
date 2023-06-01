using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : EnemyStats
{
    private Transform player;
    private Rigidbody2D rb;
    public float degrees;
    public float force;

    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0 ,0, rotation + degrees);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            this.gameObject.GetComponent<BossBullet>().enabled = false;
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
                Debug.Log("Hit player");
                Destroy(gameObject);
            }

            if (collision.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
