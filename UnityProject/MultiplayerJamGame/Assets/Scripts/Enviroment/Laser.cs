using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector2 pushOffset;
    public float knockback = 15f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("toco el jugador");
            Vector2 dir = new Vector2((collision.transform.position.x - transform.position.x) * 10f, 0) + pushOffset;
            dir = dir.normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(dir * knockback, ForceMode2D.Impulse);
            //quitarle vida
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("toco el jugador");
            Vector2 dir = (Vector2)collision.transform.position - collision.GetContact(0).point + pushOffset;
            dir = dir.normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * knockback, ForceMode2D.Impulse);
            //quitarle vida
        }
    }
}
