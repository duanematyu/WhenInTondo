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
    public BoxCollider2D standingCollider;
    public BoxCollider2D crouchingCollider;

    public bool facingRight= true;
    private bool isJumping= false;
    public bool isGrounded;
    public bool isCrouching, isLookingUp, isLookingDown, isLookingStraight;

    private float moveDirection, constantMoveSpeed;

    public float airFrictionForce;

    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    private float dashCounter, dashCoolCounter;

    public bool isMoving;

    public bool isInvulnerable;

    private void Awake()
    {
        isLookingStraight = true;
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void Start()
    {
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

        if (Input.GetButtonDown("Dash") && isMoving && isGrounded)
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                Physics2D.IgnoreLayerCollision(7, 9, true);
                isInvulnerable = true;

                constantMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                Physics2D.IgnoreLayerCollision(7, 9, false);
                isInvulnerable = false;
                constantMoveSpeed = 5;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObject);
        Movement();
    }


    private void Movement()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        isMoving = true;
        if (isLookingUp && isMoving)
        {
            playerAnim.SetBool("isLookUpWalking", true);
        }
        else
        {
            playerAnim.SetBool("isLookUpWalking", false);
        }

        if (isCrouching == true)
        {
            playerAnim.SetBool("isCrouchWalking", true);
        }
        else
        {
            playerAnim.SetBool("isCrouchWalking", false);
        }

        if (moveDirection == 0)
        {
            isMoving = false;
        }

        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        isJumping = false;
        playerAnim.SetBool("isJumping", false);
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
            playerAnim.SetBool("isJumping", true);
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
            playerAnim.SetBool("isLookingUp", true);
            isLookingUp = true;
            isLookingStraight = false;
        }

        if ((Input.GetAxis("Vertical") == 0))
        {
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
            crouchingCollider.enabled = true;
            standingCollider.enabled = false;
            isCrouching = true;
            moveSpeed = 2f;
        }

        if (Input.GetAxis("Vertical") == 0)
        {
            playerAnim.SetBool("isCrouching", false);
            isCrouching = false;
            crouchingCollider.enabled = false;
            standingCollider.enabled = true;
            moveSpeed = constantMoveSpeed;
        }
    }

    private void TargetDown()
    {
        if (Input.GetAxis("Vertical") < 0 && !isGrounded)
        {
            playerAnim.SetBool("isLookingDown", true);
            isLookingDown = true;
            isLookingStraight = false;
        }

        if (Input.GetAxis("Vertical") == 0 && isGrounded)
        {
            playerAnim.SetBool("isLookingDown", false);
            isLookingDown = false;
            isLookingStraight = true;
        }
    }
}
