using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadController : MonoBehaviour
{
    public Transform firePoint;
    public Transform[] firePoints;
    public GameObject meleePoint;
    public GameObject ammoType;
    public GameObject bomb;
    public Transform weapon;
    public Transform throwPoint;

    Quaternion rotation;

    public int ammoCount;
    public int maxAmmo;

    public float fireRate, shotCounter, throwSpeed, shotSpeed, throwCounter, force;
    public bool isFiring, isThrowing, isOutOfAmmo = false;
    private bool isInMeleeRange;
    public float playerMeleeRange = 0.5f;
    public LayerMask enemyLayerMask;

    PlayerMovement playerMovement;
    WeaponSwap weaponSwap;
    public GrenadeController grenadeController;

    public int playerDamage;

    public GameObject[] enemy;

    // Start is called before the first frame update
    void Start()
    {
        ammoCount = maxAmmo;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        playerMovement = GetComponentInParent<PlayerMovement>();
        meleePoint = GameObject.FindGameObjectWithTag("MeleePoint");
        weaponSwap = GetComponentInParent<WeaponSwap>();
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
                for(int i = 0; i < ammoCount; i++)
                {
                    Shoot();
                    ammoCount--;
                    isOutOfAmmo = false;
                }
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
            Instantiate(grenadeController, throwPoint.position, transform.rotation);
        }
        if (ammoCount == 0)
        {
            OutOfAmmo();
        }
    }

    void OutOfAmmo()
    {
        if(!isOutOfAmmo)
        {
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            weaponSwap.SwitchDefaultWeapon();
        }
        isOutOfAmmo = true;
    }

    public void AddAmmo(int amount)
    {
        ammoCount += amount;
        if (ammoCount > maxAmmo)
        {
            ammoCount = maxAmmo;
        }
    }

    void Shoot()
    {
        int playerDir()
        {
            if (transform.root.localScale.x < 0f)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        for (int i = 0; i <= 2; i++)
        {
            GameObject shot = Instantiate(ammoType, firePoint.position, Quaternion.identity);
            Rigidbody2D shotRb = shot.GetComponent<Rigidbody2D>();
            if (playerMovement.isLookingStraight && !playerMovement.isCrouching)
            {
                switch (i)
                {
                    case 0:
                        shotRb.AddForce(firePoint.right * shotSpeed + Vector3.up);
                        break;
                    case 1:
                        shotRb.AddForce(firePoint.right * shotSpeed + Vector3.up);
                        break;
                    case 2:
                        shotRb.AddForce(firePoint.right * shotSpeed + Vector3.up);
                        break;
                }
            }

            if (playerMovement.isCrouching && playerMovement.isGrounded)
            {
                switch (i)
                {
                    case 0:
                        shotRb.AddForce(firePoint.right * shotSpeed + (-Vector3.up));
                        break;
                    case 1:
                        shotRb.AddForce(firePoint.right * shotSpeed + (-Vector3.up));
                        break;
                    case 2:
                        shotRb.AddForce(firePoint.right * shotSpeed + (-Vector3.up));
                        break;
                }
            }

         /*   if (playerMovement.isLookingUp)
            {
                rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
                switch (i)
                {
                    case 0:
                        shotRb.AddForce(firePoint.up * shotSpeed);
                        break;
                    case 1:
                        shotRb.AddForce(firePoint.up * shotSpeed * 0.7f + firePoint.right * shotSpeed * 0.7f);
                        break;
                    case 2:
                        shotRb.AddForce(firePoint.up * shotSpeed * 0.7f - firePoint.right * shotSpeed * 0.7f);
                        break;
                }
            }


            if (playerMovement.isLookingDown && !playerMovement.isGrounded)
            {
                rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
    switch (i)
    {
        case 0:
            shotRb.AddForce(-firePoint.up * shotSpeed);
            break;
        case 1:
            shotRb.AddForce(-firePoint.up * shotSpeed * 0.7f + firePoint.right * shotSpeed * 0.7f);
            break;
        case 2:
            shotRb.AddForce(-firePoint.up * shotSpeed * 0.7f - firePoint.right * shotSpeed * 0.7f);
            break;
    }
            }*/
        }
       foreach (Transform firePoint in firePoints)
        {
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
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.transform.position, playerMeleeRange, enemyLayerMask);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(playerDamage);
        }
    }
}
