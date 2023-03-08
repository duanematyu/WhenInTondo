using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public GameObject weapon;
    Vector2 movement;
    Vector3 up;
    Rigidbody2D rb;
    Animator anim;

    private bool facingRight = true;
    private bool facingUp = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        Animate();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void Animate()
    {
        if (movement.x > 0 && !facingRight)
        {
            FlipCharacter();
        }

        else if (movement.x < 0 && facingRight)
        {
            FlipCharacter();
        }

        if (up.z < 0)
        {
            Debug.Log("Going up");
            LookUp();
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void LookUp()
    {
        facingUp = !facingUp;
        weapon.transform.eulerAngles = new Vector3 (0f, 0f, 90f);
    }
}
