using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldKnockback = 10f;
    public float pushKnockback = 15f;
    public float upWardEffect = 1.5f;
    public Transform pushPointA, pushPointB;
    public LayerMask pushLayers;
    public Transform player;
    public void Push()
    {
        //use stamina
        Collider2D[] colliders = Physics2D.OverlapAreaAll(pushPointA.position, pushPointB.position, pushLayers);
        foreach (Collider2D collider2D in colliders)
        {
            if (collider2D.CompareTag("Player") || collider2D.CompareTag("Enemy"))
            {
                Vector2 dir = (Vector2)collider2D.transform.position - (Vector2)player.position;
                dir = dir.normalized;
                collider2D.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushKnockback, ForceMode2D.Impulse);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("El escudo toco algo");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Defendiste, fiera");
            Vector2 dir = (Vector2)collision.transform.position - collision.GetContact(0).point
                + new Vector2(0f, upWardEffect);
            dir = dir.normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * shieldKnockback, ForceMode2D.Impulse);
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
