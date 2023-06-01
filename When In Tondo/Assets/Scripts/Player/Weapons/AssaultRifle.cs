using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssaultRifle : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject meleePoint;
    public Transform barrelTransform;
    public Transform barrelTransformUp;
    public float fireRate = 0.2f;
    public int maxAmmo = 6;

    public int currentAmmo;
    private float nextFireTime;
    public int bulletsPerShot = 3;
    public float bulletDelay = 0.1f;

    PlayerMovement playerMovement;

    private bool isInMeleeRange;
    public float playerMeleeRange = 0.5f;
    public LayerMask enemyLayerMask;

    public int currentWeaponNum = 0;
    public int playerDamage;

    public Animator playerAnim;

    WeaponSwap weaponSwap;
    AmmoCountUI ammoCountUI;

    private void Start()
    {
        weaponSwap = GetComponentInParent<WeaponSwap>();
        ammoCountUI = GetComponentInParent<AmmoCountUI>();
        playerAnim = GetComponentInParent<Animator>();
        currentAmmo = maxAmmo;
        ammoCountUI.AmmoCountUpdate(currentAmmo);
        playerMovement = GetComponentInParent<PlayerMovement>();
        meleePoint = GameObject.FindGameObjectWithTag("MeleePoint");
        ChangeWeapon();
    }

    private void Update()
    {
        isInMeleeRange = Physics2D.OverlapCircle(this.transform.position, playerMeleeRange, enemyLayerMask);
        if (Input.GetButton("Fire1") && Time.time > nextFireTime && currentAmmo > 0)
        {
            Fire();
            if (isInMeleeRange)
            {
                MeleeAttack();
            }
        }

        if (currentAmmo == 0)
        {
            weaponSwap.SwitchDefaultWeapon();
            ChangeWeapon();
            Destroy(transform.parent.gameObject);
        }
    }
    public void ChangeWeapon()
    {
        if (currentWeaponNum == 0)
        {
            currentWeaponNum += 1;
            playerAnim.SetLayerWeight(currentWeaponNum - 1, 0);
            playerAnim.SetLayerWeight(currentWeaponNum, 1);
        }

        else
        {
            currentWeaponNum -= 1;
            playerAnim.SetLayerWeight(currentWeaponNum + 1, 0);
            playerAnim.SetLayerWeight(currentWeaponNum, 1);
        }
    }

    private void Fire()
    {
        nextFireTime = Time.time + fireRate;
        currentAmmo--;
        ammoCountUI.AmmoCountUpdate(currentAmmo);

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

        for (int i = 0; i < bulletsPerShot; i++)
        {
            float angle = -1f + i * 7f / (bulletsPerShot - 1);
            Vector2 horizontalDirection = Quaternion.Euler(0f, 0f, angle) * barrelTransform.right;
            Vector2 verticalDirection = Quaternion.Euler(0f, 0f, angle) * barrelTransformUp.up;

            //shotRb.AddForce(direction * bullet.bulletSpeed * playerDir(), ForceMode2D.Impulse);

            if (playerMovement.isLookingStraight && !playerMovement.isCrouching)
            {
                GameObject shot = Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity);
                Bullet bullet = shot.GetComponent<Bullet>();
                Rigidbody2D shotRb = shot.GetComponent<Rigidbody2D>();

                shotRb.velocity = transform.right * fireRate;
                shotRb.AddForce(horizontalDirection * bullet.bulletSpeed * playerDir(), ForceMode2D.Impulse);
            }

            if (playerMovement.isCrouching && playerMovement.isGrounded)
            {
                GameObject shot = Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity);
                Bullet bullet = shot.GetComponent<Bullet>();
                Rigidbody2D shotRb = shot.GetComponent<Rigidbody2D>();

                shotRb.AddForce(horizontalDirection * bullet.bulletSpeed * playerDir(), ForceMode2D.Impulse);
            }

            if (playerMovement.isLookingUp)
            {
                GameObject shotUp = Instantiate(bulletPrefab, barrelTransformUp.position, Quaternion.identity);
                Bullet bullet = shotUp.GetComponent<Bullet>();
                Rigidbody2D shotRbUp = shotUp.GetComponent<Rigidbody2D>();

                shotRbUp.AddForce(verticalDirection * bullet.bulletSpeed * playerDir(), ForceMode2D.Impulse);
            }

            if (playerMovement.isLookingDown && !playerMovement.isGrounded)
            {
                GameObject shotUp = Instantiate(bulletPrefab, barrelTransformUp.position, Quaternion.identity);
                Rigidbody2D shotRbUp = shotUp.GetComponent<Rigidbody2D>();
                Bullet bullet = shotUp.GetComponent<Bullet>();

                shotRbUp.AddForce(-verticalDirection * bullet.bulletSpeed * playerDir(), ForceMode2D.Impulse);
            }
            StartCoroutine(DelayBulletInstantiate());
        }
    }

    void MeleeAttack()
    {
        playerAnim.SetTrigger("stab");
        StartCoroutine(ResetStab());
        Debug.Log("Player Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.transform.position, playerMeleeRange, enemyLayerMask);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(playerDamage);
        }
    }
    private IEnumerator DelayBulletInstantiate()
    {
        yield return new WaitForSeconds(bulletDelay);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, playerMeleeRange);
    }

    IEnumerator ResetStab()
    {
        yield return new WaitForSeconds(1f);
        playerAnim.ResetTrigger("stab");
    }
}
