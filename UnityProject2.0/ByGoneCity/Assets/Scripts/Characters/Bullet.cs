using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeTime;
    private void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().ActualHealth -= damage;
        }
        //else if (CompareTag("Lever"))
        //{

        //}
        Destroy(gameObject);
    }
}
