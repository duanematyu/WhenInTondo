using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    public float playerInRange;
    public bool isInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (player.position.x < transform.position.x)
            {
                if (player.localScale.x == -1)
                {
                    TurnAround();
                }
            }

            if (player.position.x > transform.position.x)
            {
                if (player.localScale.x == 1)
                {
                    TurnAround();
                }
            }

            if (Vector2.Distance(transform.position, player.position) < playerInRange)
            {
                isInRange = true;
            }

            if (Vector2.Distance(transform.position, player.position) > playerInRange)
            {
                isInRange = false;
            }
        }
       
    }

    void TurnAround()
    {
        float vecToTarget = player.transform.position.x - transform.position.x;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(vecToTarget), transform.localScale.y, transform.localScale.z);
    }
}
