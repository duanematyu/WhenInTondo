using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header ("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    public bool isInvulnerable;

    public GameObject player;

    //public GameObject GameOverScreen;
    //public GameObject HealthHud;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spriteRend = GetComponent<SpriteRenderer>();
        //player = gameObject.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
            //animator.SetTrigger("isDead");
            //GameOverScreen.SetActive(true);
            //HealthHud.SetActive(false);
            //player.enabled = false;
            //gunScript.enabled = false;
        }

        HealthSlider.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        //animator.SetTrigger("isHurt");
        currentHealth -= amount;
        StartCoroutine(Invulnerability());
    }

    public void Die()
    {
        //animator.SetBool("isDead", true);
        isDead = true;
        currentHealth = 0;
        player.SetActive(false);
        //Destroy(gameObject);
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
}
