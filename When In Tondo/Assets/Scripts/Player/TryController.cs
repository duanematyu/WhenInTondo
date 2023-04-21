using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;

    private bool facingRight = true;

    Vector2 aimDirection;
    //Vector2 movement;

    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }
    void Update()
    {
       // movement.x = Input.GetAxisRaw("Horizontal");
        //Animate();
        aimDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (playerMovement.isCrouching && playerMovement.isGrounded)
        {
            aimDirection.y = 0f;
        }
        Debug.Log(aimDirection);

        if (Input.GetButtonDown("Fire1"))
        {
            // Calculate the angle of the aim direction in radians
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x);

            // Instantiate a bullet prefab and set its position to the bullet spawn point
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

            // Set the bullet's velocity based on the aim direction
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Mathf.Cos(angle), bulletSpeed * Mathf.Sin(angle));
        }
    }

   /* private void Animate()
    {
        if (movement.x > 0 && !facingRight)
        {
            FlipCharacter();
        }

        else if (movement.x < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }*/
}

