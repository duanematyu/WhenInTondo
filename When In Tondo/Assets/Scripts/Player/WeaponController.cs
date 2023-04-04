using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform firePoint;
    public Transform meleePoint;
    public GameObject ammoType;
    public GameObject bomb;
    public Transform weapon;
    public Transform throwPoint;

    public float fireRate, shotCounter, throwSpeed, shotSpeed, throwCounter, force;
    public bool isFiring, isThrowing;
    private bool isInMeleeRange;
    public float playerMeleeRange = 0.5f;
    public LayerMask enemyLayerMask;

    PlayerMovement playerMovement;
    public GrenadeController grenadeController;

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
        isInMeleeRange = Physics2D.OverlapCircle(transform.position, playerMeleeRange, enemyLayerMask);
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

        if(Input.GetButtonDown("Fire2"))
        {
           Instantiate(grenadeController, throwPoint.position, transform.rotation);
        }
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

   
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, playerMeleeRange);
    }


    void MeleeAttack()
    {
        Debug.Log("Player Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, playerMeleeRange, enemyLayerMask);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(playerDamage);
        }
    }
}
