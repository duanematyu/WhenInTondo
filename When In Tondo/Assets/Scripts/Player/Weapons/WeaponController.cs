using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject meleePoint;
    public GameObject ammoType;
    public GameObject bomb;
    public Transform weapon;
    public Transform throwPoint;
    Vector2 aimDirection;

    public float fireRate, shotCounter, throwSpeed, shotSpeed, throwCounter, force, molotovCount, currentMolotovCount;
    public bool isFiring, isThrowing;
    private bool isInMeleeRange;
    public float playerMeleeRange = 0.5f;
    public LayerMask enemyLayerMask;
    public Animator playerAnim;
    PlayerMovement playerMovement;
    public GrenadeController grenadeController;
    public int playerDamage;
    public int currentWeaponNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerAnim = GetComponentInParent<Animator>();
        meleePoint = GameObject.FindGameObjectWithTag("MeleePoint");
        //molotovAmmo = GameObject.FindGameObjectWithTag("MolotovText");
        //enemyStats = enemy.GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
       /* aimDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (playerMovement.isCrouching && playerMovement.isGrounded)
        {
            aimDirection.y = 0f;
        }*/

        isInMeleeRange = Physics2D.OverlapCircle(this.transform.position, playerMeleeRange, enemyLayerMask);
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
        
       /* if(Input.GetKeyDown(KeyCode.C))
        {
            ChangeWeapon();
        }*/

        /*if(Input.GetButtonDown("Fire2"))
        {
           for(int i = 0; i < molotovCount; i++)
           {
             Instantiate(grenadeController, throwPoint.position, transform.rotation);
             molotovCount--;
           }
        } */  
    }

    void Shoot()
    {
        /* // Calculate the angle of the aim direction in radians
         float angle = Mathf.Atan2(aimDirection.y, aimDirection.x);

         // Instantiate a bullet prefab and set its position to the bullet spawn point
         GameObject bullet = Instantiate(ammoType, firePoint.position, Quaternion.identity);

         // Set the bullet's velocity based on the aim direction
         bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shotSpeed * Mathf.Cos(angle), shotSpeed * Mathf.Sin(angle));*/

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
        playerAnim.SetTrigger("stab");
        StartCoroutine(ResetStab());
        Debug.Log("Player Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.transform.position, playerMeleeRange, enemyLayerMask);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(playerDamage);
        }
    }

    IEnumerator ResetStab()
    {
        yield return new WaitForSeconds(1f);
        playerAnim.ResetTrigger("stab");
    }

   public void ChangeWeapon()
    {
        if(currentWeaponNum == 0)
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
}
