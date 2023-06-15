using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    BossHealth bossHealth;
    public GameObject boss;
    public GameObject stageClear;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossHealth = boss.GetComponent<BossHealth>();
    }

    private void Update()
    {
        if(bossHealth.isDead)
        {
            stageClear.SetActive(true);
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(1);
    }
}
