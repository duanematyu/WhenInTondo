using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPickup : MonoBehaviour
{
    WeaponPickup weaponPickup;
    public GameObject weapon;
    private void Start()
    {
        weaponPickup = weapon.GetComponent<WeaponPickup>();
    }
    public void UpdateWeapon(GameObject newWeapon)
    {
        weapon = newWeapon;
        weaponPickup = weapon.GetComponent<WeaponPickup>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

        }
    }
}
