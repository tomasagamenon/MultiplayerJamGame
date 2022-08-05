using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Vector2 pushOffset;
    public float knockback = 10f;
    public int damage = 10;
    public float invulTime = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 dir = (Vector2)collision.transform.position - collision.GetContact(0).point + pushOffset;
            dir = dir.normalized;
            rb.AddForce(dir * knockback, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Health>().Damage(damage, invulTime);
            //sound?
        }
    }
}
