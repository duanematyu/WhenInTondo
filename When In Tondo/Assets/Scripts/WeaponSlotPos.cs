using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotPos : MonoBehaviour
{
    PlayerMovement player;
    WeaponController weaponC;
    public GameObject bulletPos;
    [SerializeField] Vector2 standPos, crouchPos, lookingUp, lookingDown;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        weaponC = GetComponent<WeaponController>();
        standPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    { 
        if (player.isCrouching)
        {
            transform.localPosition = crouchPos;
        }

        if (player.isLookingUp)
        {
            transform.localPosition = lookingUp;
        }

        if (player.isLookingDown)
        {
            transform.localPosition = lookingDown;
        }

        if(!player.isCrouching && !player.isLookingUp && !player.isLookingDown)
        {
            transform.localPosition = standPos;
            //weaponC.constant = bulletPos.transform;
        }
    }
}
