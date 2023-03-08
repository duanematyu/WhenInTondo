using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    float timeBetween;
    public float startTimeBetween;

    EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponentInChildren<EnemyController>();
        timeBetween = startTimeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetween <= 0 && enemyController.isInRange)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            timeBetween = startTimeBetween;
        }
        else
        {
            timeBetween -= Time.deltaTime;
        }
    }
}
