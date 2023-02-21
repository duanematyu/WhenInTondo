using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed, jumpForce, checkRadius;

    public Transform overheadCheck, groundCheck;
    public LayerMask groundObject;
    public Animator playerAnim;

    private Rigidbody2D rb;
    private bool facingRight= true;
    private bool isJumping= false;
    private bool isGrounded;
    public bool isCrouching, isLookingUp, isLookingDown, isLookingStraight;

    private float moveDirection, constantMoveSpeed;

    Vector3 oldRotation;
    Vector2 direction;

    public GameObject bulletPos;

    WeaponController weaponC;
    private void Awake()
    {
        isLookingStraight = true;
        rb = GetComponent<Rigidbody2D>();
        weaponC = bulletPos.GetComponent<WeaponController>();
        constantMoveSpeed = moveSpeed;
    }

 
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Animate();
        Crouch();
        LookUp();
        TargetDown();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObject);

        Movement();
    }

    private void Movement()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if(isJumping)
        {
            //rb.AddForce(new Vector2(0f, jumpForce));
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        isJumping = false;
        isLookingDown = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }

        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        playerAnim.SetFloat("speed", Mathf.Abs(moveDirection));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void LookUp()
    {
        if ((Input.GetAxis("Vertical") > 0))
        {
            /*oldRotation = transform.eulerAngles;
            weaponC.firePoint.transform.eulerAngles = new Vector3(0, 0, 90f);
            weaponC.firePoint.transform.eulerAngles = new Vector3(0, 0, 90f);*/
            playerAnim.SetBool("isLookingUp", true);
            isLookingUp = true;
            isLookingStraight = false;
        }

        if ((Input.GetAxis("Vertical") == 0))
        {
            /*weaponC.firePoint.transform.eulerAngles = oldRotation;
            weaponC.firePoint.transform.eulerAngles = oldRotation;*/
            playerAnim.SetBool("isLookingUp", false);
            isLookingUp = false;
            isLookingStraight = true;
        } 
    }

    private void Crouch()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            playerAnim.SetBool("isCrouching", true);
            isCrouching = true;
            moveSpeed = 2f;
        }

        if (Input.GetAxis("Vertical") == 0)
        {
            playerAnim.SetBool("isCrouching", false);
            isCrouching = false;
            moveSpeed = constantMoveSpeed;
        }
    }

    private void TargetDown()
    {
        if (Input.GetAxis("Vertical") < 0 && !isGrounded)
        {
            /*oldRotation = transform.eulerAngles;
           weaponC.firePoint.transform.eulerAngles = new Vector3(0, 0, -90f);*/
            isLookingDown = true;
            isLookingStraight = false;
        }

        if (Input.GetAxis("Vertical") == 0 && isGrounded)
        {
             /*weaponC.firePoint.transform.eulerAngles = oldRotation;
            weaponC.firePoint.transform.eulerAngles = oldRotation;*/
            isLookingDown = false;
            isLookingStraight = true;
        }
    }
}
