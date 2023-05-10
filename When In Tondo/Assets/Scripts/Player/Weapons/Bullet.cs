using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    PlayerMovement playerMovement;
    public int damage;

    private Rigidbody2D bulletRb;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        //bulletRb = GetComponent<Rigidbody2D>();
       // bulletRb.velocity = transform.right * bulletSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collisionGameObject = other.gameObject;

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (collisionGameObject.GetComponent<EnemyStats>() != null)
            {
                collisionGameObject.GetComponent<EnemyStats>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
