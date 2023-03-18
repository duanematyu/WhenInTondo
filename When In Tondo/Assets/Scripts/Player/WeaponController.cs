using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject ammoType;
    public GameObject bomb;
    public Transform weapon;

    public float playerRange;
    public float fireRate, shotCounter, throwSpeed, shotSpeed, throwCounter;
    public bool isFiring, isThrowing;
    private bool isInMeleeRange;
    public LayerMask enemyLayerMask;

    PlayerMovement playerMovement;

    public int playerDamage;

    EnemyStats enemyStats;

    public GameObject[] enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        playerMovement = GetComponentInParent<PlayerMovement>();
        //enemyStats = enemy.GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        isInMeleeRange = Physics2D.OverlapCircle(transform.position, playerRange, enemyLayerMask);
        if (Input.GetButtonDown("Fire1"))
        {
            shotCounter -= Time.deltaTime;
            if (isInMeleeRange)
            {
                MeleeAttack();
            }

            else if (shotCounter <= 0)
            {
                shotCounter = fireRate;
                Shoot();
                isFiring = true;
            }
            isFiring = false;
        }
        else
        {
            shotCounter = 0;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            throwCounter -= Time.deltaTime;
            if (throwCounter <= 0)
            {
                throwCounter = fireRate;
                Throw();
                isThrowing = true;
            }
            isThrowing = false;
        }

        else
        {
            throwCounter = 0;
        }
    }

    void MeleeAttack()
    {
        Debug.Log("Attack melee");
        enemyStats.TakeDamage(playerDamage);
    }

    void Shoot()
    {
        int playerDir()
        {
            if (transform.parent.localScale.x < 0f)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        GameObject shot = Instantiate(ammoType, firePoint.position, Quaternion.identity);
        Rigidbody2D shotRb = shot.GetComponent<Rigidbody2D>();

        if (playerMovement.isLookingStraight && !playerMovement.isCrouching)
        {
            shotRb.AddForce(firePoint.right * shotSpeed * playerDir(), ForceMode2D.Impulse);
        }

        if (playerMovement.isCrouching && playerMovement.isGrounded)
        {
            shotRb.AddForce(firePoint.right * shotSpeed * playerDir(), ForceMode2D.Impulse);
        }

        if (playerMovement.isLookingUp)
        {
            shotRb.AddForce(firePoint.up * shotSpeed * playerDir(), ForceMode2D.Impulse);
        }

        if (playerMovement.isLookingDown && !playerMovement.isGrounded)
        {
            shotRb.AddForce(-firePoint.up * shotSpeed * playerDir(), ForceMode2D.Impulse);
        }
    }

    void Throw()
    {
        int playerDir()
        {
            if (transform.parent.localScale.x < 0f)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        GameObject grenade = Instantiate(bomb, firePoint.position, transform.rotation);
        Rigidbody2D grenadeRb = bomb.GetComponent<Rigidbody2D>();
        grenadeRb.AddForce(firePoint.right * throwSpeed * playerDir(), ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, playerRange);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collisionGameObject = other.gameObject;

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (collisionGameObject.GetComponent<EnemyStats>() != null)
            {
                collisionGameObject.GetComponent<EnemyStats>().TakeDamage(playerDamage);
            }
            Destroy(gameObject);
        }
    }
}
