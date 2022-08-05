using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector2 pushOffset;
    public float knockback = 15f;
    public int damage = 10;
    public float invulTime = 1f;
    public float stunTime = 0.5f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("toco el jugador");
            Vector2 dir = (Vector2)collision.gameObject.GetComponent<Health>().playerCentre.position - collision.GetContact(0).point + pushOffset;
            //dir = -dir.normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * knockback, ForceMode2D.Impulse);
            //collision.gameObject.GetComponent<Health>().Damage(damage, invulTime, stunTime);
            //quitarle vida
        }
    }
}
