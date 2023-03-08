using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject ammoType;
    public GameObject bomb;
    public Transform weapon;

    public float fireRate, shotCounter, throwSpeed, shotSpeed, throwCounter;
    public bool isFiring, isThrowing;

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

        if(Input.GetButtonDown("Fire2"))
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

        if(playerMovement.isLookingStraight && !playerMovement.isCrouching)
        {
            shotRb.AddForce(firePoint.right * shotSpeed * playerDir(), ForceMode2D.Impulse);
        }

        if (playerMovement.isCrouching && playerMovement.isGrounded)
        {
            shotRb.AddForce(firePoint.right * shotSpeed * playerDir(), ForceMode2D.Impulse);
        }

        if(playerMovement.isLookingUp)
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
