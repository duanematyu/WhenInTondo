using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 500;
    [SerializeField]
    private float currentHealth;

    public float waitTime = 2f;

    public Slider HealthSlider;

    public HealthBar healthBar;


    public GameObject boss;
    public GameObject slider;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
 
        slider = GameObject.FindGameObjectWithTag("BossHealth");
        HealthSlider = slider.GetComponent<Slider>();
        healthBar = slider.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        boss = GameObject.FindGameObjectWithTag("Boss");
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
        }

        HealthSlider.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

    public void Die()
    {
        isDead = true;
        currentHealth = 0;
        Destroy(gameObject);
        Destroy(slider);
    }
}
