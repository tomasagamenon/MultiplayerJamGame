using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector2 pushOffset;
    public float knockback = 15f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("toco el jugador");
            Vector2 dir = (Vector2)collision.transform.position - 
                new Vector2(collision.GetContact(0).point.x * 2, collision.GetContact(0).point.y) + pushOffset;
            dir = dir.normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * knockback, ForceMode2D.Impulse);
            //quitarle vida
        }
    }
}
