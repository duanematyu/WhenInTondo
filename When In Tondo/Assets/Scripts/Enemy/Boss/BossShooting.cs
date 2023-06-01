using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    public float shotCounter;
    public float fireRate;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }

   public void Shoot()
    {
        /*if (Time.time > shotCounter)
        {
            
        }*/
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
