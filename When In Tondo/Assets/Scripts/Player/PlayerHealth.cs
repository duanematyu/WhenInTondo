using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private float currentHealth;
    //public PlayerScript player;
    public Animator animator;

    public float waitTime = 2f;

    public Slider HealthSlider;

    public HealthBar healthBar;

    public TextMeshProUGUI livesText;

    public int lives =3;

    Vector2 startPos;

    [Header ("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    public bool isInvulnerable;

    public GameObject player;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spriteRend = GetComponent<SpriteRenderer>();
        livesText.text = "x" + lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        startPos = transform.position + new Vector3(0,1f,0);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0 && lives > 0)
        {
            Die();
        }

        HealthSlider.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        StartCoroutine(Invulnerability());
    }

    public void Die()
    {
        lives--;
        livesText.text = "x" + lives.ToString();
        if(lives == 0)
        {
            isDead = true;
            animator.SetTrigger("StabDeath");
            Invoke("SuspendPlayer", 1);
        }
        currentHealth = maxHealth;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        StartCoroutine(Invulnerability());
        transform.position = startPos;
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(7, 9, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            isInvulnerable = true;
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(1);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(1);
        }
        Physics2D.IgnoreLayerCollision(7, 9, false);
        isInvulnerable = false;
    }

    void SuspendPlayer()
    {
        player.SetActive(false);
        Destroy(player);
    }

}
