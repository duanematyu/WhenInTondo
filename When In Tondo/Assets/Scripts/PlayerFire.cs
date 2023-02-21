using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform bulletPosition;
    public Transform weapon;
    public GameObject bullet;
    public Animator playerAnim;

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        //LookUp();

        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, bulletPosition.position, bulletPosition.rotation);
        }
    }
}
