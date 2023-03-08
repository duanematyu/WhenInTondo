using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    PlayerMovement playerMovement;

    private Rigidbody2D bulletRb;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        //bulletRb = GetComponent<Rigidbody2D>();
        //bulletRb.velocity = transform.right * bulletSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Instantiate(transform.position, Quaternion.identity);
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
