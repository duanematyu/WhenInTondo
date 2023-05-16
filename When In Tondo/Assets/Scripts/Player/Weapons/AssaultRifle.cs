using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform barrelTransform;
    public Transform barrelTransformUp;
    public float fireRate = 0.2f;
    public int maxAmmo = 6;

    public int currentAmmo;
    private float nextFireTime;
    public int bulletsPerShot = 3;
    public float bulletDelay = 0.1f;

    PlayerMovement playerMovement;

    private void Start()
    {
        currentAmmo = maxAmmo;
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime && currentAmmo > 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        nextFireTime = Time.time + fireRate;
        currentAmmo--;

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

    private IEnumerator DelayBulletInstantiate()
    {
        yield return new WaitForSeconds(bulletDelay);
    }
}
