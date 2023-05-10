using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotPos : MonoBehaviour
{
    PlayerMovement player;
    [SerializeField] Vector2 standPos, crouchPos, lookingUp, lookingDown;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        standPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    { 
        if (player.isCrouching)
        {
            transform.localPosition = crouchPos;
        }

        if(!player.isCrouching)
        {
            transform.localPosition = standPos;
            //weaponC.constant = bulletPos.transform;
        }
    }
}
