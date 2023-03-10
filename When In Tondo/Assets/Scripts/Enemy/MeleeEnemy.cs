using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyStats
{
    private Transform player;
    public float speed;
    public bool isInRange = false;
    public float playerInRange;
    public float attackRange;
    private bool isInAttackRange;
    public SpriteRenderer renderer;
    public bool facingRight = false;
    PlayerHealth playerHealth;

    private float canAttack = 0f;
    private float attackSpeed = .5f;

    public LayerMask playerLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        renderer = GetComponent<SpriteRenderer>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, playerLayerMask);

            Vector3 scale = transform.localScale;

            if (isInRange)
            {
                Follow();
            }

            if (Vector2.Distance(transform.position, player.position) < attackRange && player != null)
            {
                isInAttackRange = true;
                Attack();
            }

            if (Vector2.Distance(transform.position, player.position) > attackRange)
            {
                isInAttackRange = false;
            }

            if (player.position.x < transform.position.x)
            {
                renderer.flipX = true;
                if (player.localScale.x == -1)
                {
                    TurnAround();
                }
            }

            if (player.position.x > transform.position.x)
            {
                renderer.flipX = false;
                if (player.localScale.x == 1)
                {
                    TurnAround();
                }
            }

            transform.localScale = scale;

            if (Vector2.Distance(transform.position, player.position) < playerInRange)
            {
                isInRange = true;
            }

            if (Vector2.Distance(transform.position, player.position) > playerInRange)
            {
                isInRange = false;
            }
        }
        
    }

    void TurnAround()
    {
        float vecToTarget = player.transform.position.x - transform.position.x;
        if (player != null)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(vecToTarget), transform.localScale.y, transform.localScale.z);
        }
    }

    void Attack()
    {
        Debug.Log("Attacking player");
        if(attackSpeed <= canAttack)
        {
           playerHealth.TakeDamage(baseAttack);
            canAttack = 0f;
        }

        else
        {
            canAttack += Time.deltaTime;
        }
    }

    void Follow()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
    }
           
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
