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
    public Animator meleeEnemyAnim;

    public GameObject spawner;

    public Rigidbody2D rb;

    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    EnemySpawner enemySpawner;

    private float canAttack = 0f;
    private float attackSpeed = .5f;

    public LayerMask playerLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindWithTag("Spawner");
        enemySpawner = spawner.GetComponent<EnemySpawner>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        renderer = GetComponent<SpriteRenderer>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
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

            if (health <= 0)
            {
                Death();
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
        if(attackSpeed <= canAttack && !playerHealth.isInvulnerable && !playerMovement.isInvulnerable)
        {
            meleeEnemyAnim.SetTrigger("Stab");
            FindObjectOfType<AudioManager>().Play("MananaksakStab");
            meleeEnemyAnim.ResetTrigger("Walk");
            StartCoroutine(ResetStab());
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

    private void Death()
    {
        enemySpawner.OnDeath();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Jumpable" && isInRange)
        {
            rb.AddForce(Vector2.up * 450f);
        }

        else if (collision.tag == "HigherJumpable" && isInRange)
        {
            rb.AddForce(Vector2.up * 600f);
        }
    }

    IEnumerator ResetStab()
    {
        yield return new WaitForSeconds(.2f);
        meleeEnemyAnim.ResetTrigger("Stab");
        meleeEnemyAnim.SetTrigger("Walk");
    }
}
