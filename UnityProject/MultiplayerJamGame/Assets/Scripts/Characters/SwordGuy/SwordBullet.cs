using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 30;
    public Rigidbody2D rb;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyLife>().TakeDamage(damage);
            Destroy(gameObject);
        } else if (collision.CompareTag("Lever"))
        {
            collision.GetComponent<Lever>().InteractLever();
        }
        Debug.Log("Golpeaste " + collision.name);
        //some exploding animation
    }
}
