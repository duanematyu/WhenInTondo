using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSwap : MonoBehaviour
{
    public Transform weaponSlot;
    public GameObject activeWeapon;
    public GameObject pistol;
    public int plusAmmo;

    public TextMeshProUGUI ammoCount;

    // Start is called before the first frame update
    void Start()
    {
        var weapon = Instantiate(activeWeapon, weaponSlot.transform.position, weaponSlot.transform.rotation);
        weapon.transform.parent = weaponSlot.transform;
    }

    public void SwitchDefaultWeapon()
    {
        var weapon = Instantiate(pistol, weaponSlot.transform.position, weaponSlot.transform.rotation);
        weapon.transform.parent = weaponSlot.transform;
        ammoCount.text = "-";
    }

    public void UpdateWeapon(GameObject newWeapon)
    {
        if (newWeapon.name == activeWeapon.name)
        {
            activeWeapon.GetComponent<SpreadController>().AddAmmo(plusAmmo);
        }
        activeWeapon = newWeapon;
        
        var weapon = Instantiate(activeWeapon, weaponSlot.transform.position, weaponSlot.transform.rotation);
        weapon.transform.parent = weaponSlot.transform;
    }
}
