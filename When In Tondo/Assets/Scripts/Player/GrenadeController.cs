using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrenadeController : MonoBehaviour
{
    public float speed = 4;
    public Vector3 LaunchOffset;
    public bool thrown;
    public GameObject firePrefab;
    //public int molotovCount = 10;

    private void Start()
    {
        if(thrown)
        {
            var direction = transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        }
        transform.Translate(LaunchOffset);        
    }
    private void Update()
    {
        if(!thrown)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Vector3 collisionPoint = collision.contacts[0].point;
            Vector3 normalVector = collision.contacts[0].normal;
            Quaternion fireRotation = Quaternion.LookRotation(Vector3.forward, normalVector);
            Instantiate(firePrefab, collisionPoint, fireRotation);
            Destroy(GameObject.Find("groundfire(Clone)"), 2f);
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
   }
}
