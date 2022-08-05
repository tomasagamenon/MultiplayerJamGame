using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debree : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;
    public int damage = 5;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Golpeo un jugador");
            collision.gameObject.GetComponent<Health>().Damage(damage);
            //special animation?
        }
        //break
        //send to pool/destroy
        Destroy(gameObject);
    }
}
