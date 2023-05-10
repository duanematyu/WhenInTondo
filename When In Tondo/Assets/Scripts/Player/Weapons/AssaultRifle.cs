using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform barrelTransform;
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
            GameObject shot = Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity);
            Rigidbody2D shotRb = shot.GetComponent<Rigidbody2D>();

            shotRb.velocity = transform.right * fireRate;

            float angle = -15f + i * 15f / (bulletsPerShot - 1);
            Vector2 direction = Quaternion.Euler(0f, 0f, angle) * barrelTransform.right;

            shotRb.AddForce(direction * fireRate * playerDir(), ForceMode2D.Impulse);
            

            StartCoroutine(DelayBulletInstantiate());
        }
    }

    private IEnumerator DelayBulletInstantiate()
    {
        yield return new WaitForSeconds(bulletDelay);
    }
}
