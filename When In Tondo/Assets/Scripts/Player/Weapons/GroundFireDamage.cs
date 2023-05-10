using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFireDamage : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collisionGameObject.GetComponent<EnemyStats>() != null)
            {
                collisionGameObject.GetComponent<EnemyStats>().TakeDamage(damage);
            }
        }
    }
}
