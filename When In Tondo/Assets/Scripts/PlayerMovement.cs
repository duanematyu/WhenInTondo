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
    public bool isGrounded;
    public bool isCrouching, isLookingUp, isLookingDown, isLookingStraight;

    private float moveDirection, constantMoveSpeed;

    public float airFrictionForce;

    Vector3 oldRotation;
    Vector2 direction;

    public GameObject bulletPos;

    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    private float dashCounter, dashCoolCounter;

    public bool isMoving;

    private TrailRenderer trailRenderer;

    private Vector2 dashingDir;

    /* [Header("Dashing")]
     private TrailRenderer trailRenderer;
     [SerializeField] private float dashingVel = 30f;
     [SerializeField] private float dashingTime = .5f;

     private bool isDashing;
     private bool canDash = true;*/


    WeaponController weaponC;
    private void Awake()
    {
        isLookingStraight = true;
        rb = GetComponent<Rigidbody2D>();
        weaponC = bulletPos.GetComponent<WeaponController>();  
    }

    private void Start()
    {
        constantMoveSpeed = moveSpeed;
        trailRenderer = GetComponent<TrailRenderer>();
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
                trailRenderer.emitting = true;
                Physics2D.IgnoreLayerCollision(0, 6, true);

                constantMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                constantMoveSpeed = 5;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
            trailRenderer.emitting = false;
            Physics2D.IgnoreLayerCollision(0, 6, false);
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

        if (moveDirection == 0)
        {
            isMoving = false;
        }

        if (isJumping)
        {
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
            isLookingDown = true;
            isLookingStraight = false;
        }

        if (Input.GetAxis("Vertical") == 0 && isGrounded)
        {
            isLookingDown = false;
            isLookingStraight = true;
        }
    }
}
