using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float degrees;
    public float force;
    public float bulletDamage;

    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Instantiate(transform.position, Quaternion.identity);
        if (player != null)
        {
            if (collision.tag == "Player" && !playerHealth.isInvulnerable && !playerMovement.isInvulnerable)
            {
                playerHealth.TakeDamage(bulletDamage);
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
