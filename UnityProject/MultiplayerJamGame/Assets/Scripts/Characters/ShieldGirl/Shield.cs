using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldKnockback = 10f;
    public float shieldUpwardEffect = 1.5f;
    public float shieldCost = 5f;
    public float pushKnockback = 15f;
    public float pushUpwardEffect = 2.0f;
    public float pushCost = 15f;
    public Transform pushPointA, pushPointB;
    public Collider2D pushCollider;
    public LayerMask pushLayers;
    public Transform player;
    private Stamina stamina;
    private void Awake()
    {
        stamina = player.GetComponent<Stamina>();
    }
    public void Push()
    {
        if (stamina.EnoughStamina(pushCost))
        {
            Collider2D[] colliders = Physics2D.OverlapAreaAll(pushPointA.position, pushPointB.position, pushLayers);
            foreach (Collider2D collider2D in colliders)
            {
                if (collider2D.CompareTag("Player") || collider2D.CompareTag("Enemy"))
                {
                    if (stamina.EnoughStamina(pushCost))
                    {
                        Vector2 dir = (Vector2)collider2D.transform.position - (Vector2)player.position;
                        dir = dir.normalized;
                        collider2D.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushKnockback, ForceMode2D.Impulse);
                    }
                }
            }
            stamina.SpendStamina(pushCost);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.CompareTag("Enemy") && stamina.EnoughStamina(shieldCost))
        {
            Vector2 dir = (Vector2)collision.transform.position - collision.GetContact(0).point
                + new Vector2(0f, shieldUpwardEffect);
            dir = dir.normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * shieldKnockback, ForceMode2D.Impulse);
            stamina.SpendStamina(shieldCost);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pushPointA.position, pushPointB.position);
        Gizmos.DrawLine(pushPointA.position, new Vector3(pushPointB.position.x, pushPointA.position.y));
        Gizmos.DrawLine(pushPointA.position, new Vector3(pushPointA.position.x, pushPointB.position.y));
        Gizmos.DrawLine(pushPointB.position, new Vector3(pushPointB.position.x, pushPointA.position.y));
        Gizmos.DrawLine(pushPointB.position, new Vector3(pushPointA.position.x, pushPointB.position.y));
    }
}
