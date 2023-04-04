using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    private bool isInMeleeRange;
    public float playerRange;
    public LayerMask enemyLayerMask;
    public int meleeDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isInMeleeRange = Physics2D.OverlapCircle(transform.position, playerRange, enemyLayerMask);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collisionGameObject = other.gameObject;

        if(Input.GetButtonDown("Fire3") && isInMeleeRange)
        {
            if(other.gameObject.CompareTag("Enemy"))
            {
                if (collisionGameObject.GetComponent<EnemyStats>() != null)
                {
                    Debug.Log("Colliding");
                    collisionGameObject.GetComponent<EnemyStats>().TakeDamage(meleeDamage);
                }
            }
        }  
    }

    }
