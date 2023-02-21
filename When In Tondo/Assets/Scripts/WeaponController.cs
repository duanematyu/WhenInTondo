using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject ammoType;
    public Transform weapon;

    public float shotSpeed;
    public float fireRate, shotCounter;
    public bool isFiring;

    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
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
    }

    void Shoot()
    {
        int playerDir()
        {
            if(transform.parent.localScale.x < 0f)
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

        if(playerMovement.isLookingStraight)
        {
            shotRb.AddForce(firePoint.right * shotSpeed * playerDir(), ForceMode2D.Impulse);
        }

        if(playerMovement.isLookingUp)
        {
            shotRb.AddForce(firePoint.up * shotSpeed * playerDir(), ForceMode2D.Impulse);
        }

        if (playerMovement.isLookingDown)
        {
            shotRb.AddForce(-firePoint.up * shotSpeed * playerDir(), ForceMode2D.Impulse);
        }
        shotRb.AddForce(firePoint.right * shotSpeed * playerDir(), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
