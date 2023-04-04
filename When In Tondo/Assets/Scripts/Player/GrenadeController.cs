using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public float speed = 4;
    public Vector3 LaunchOffset;
    public bool thrown;
    public GameObject firePrefab;

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
            Debug.Log("Hit");
            Vector3 collisionPoint = collision.contacts[0].point;
            Vector3 wallPosition = collision.gameObject.transform.position;
            float yOffset = Mathf.Abs(collisionPoint.y - wallPosition.y) / .8f;
            Instantiate(firePrefab, new Vector3(collisionPoint.x, wallPosition.y + yOffset, collisionPoint.z), Quaternion.identity);
            Destroy(GameObject.Find("groundfire(Clone)"), 2f);
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
   }
}
