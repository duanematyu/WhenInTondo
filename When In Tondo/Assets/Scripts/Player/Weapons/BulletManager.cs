using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private int activeBulletCount;

    private void Start()
    {
        activeBulletCount = transform.childCount;
    }

    private void Update()
    {
        // Check if all bullets are inactive
        if (activeBulletCount == 0)
        {
            // Destroy the parent object
            Destroy(gameObject);
        }
    }

    public void DecrementBulletCount()
    {
        activeBulletCount--;
    }
}
