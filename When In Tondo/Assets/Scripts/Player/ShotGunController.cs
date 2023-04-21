using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunController : MonoBehaviour
{
    public Transform firepoint;
    public GameObject firepoint1;
    public GameObject firepoint2;
    public float attackRange = 50f;
    public LayerMask enemyLayer;
    public int shotgunDamage;

    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        if(playerMovement.isLookingUp)
        {
            firepoint2.SetActive(true);
        }
    }

    void Fire()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firepoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(shotgunDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firepoint == null)
            return;

        Gizmos.DrawWireSphere(firepoint.position, attackRange);
    }
}

