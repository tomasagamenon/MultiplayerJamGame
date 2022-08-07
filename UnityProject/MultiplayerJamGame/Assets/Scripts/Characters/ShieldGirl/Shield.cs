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
    public Collider2D pushCollider;
    public LayerMask pushLayers;
    public Transform player;
    private Stamina stamina;
    private AudioManager audioManager;
    private void Awake()
    {
        stamina = player.GetComponent<Stamina>();
        audioManager = GetComponent<AudioManager>();
    }
    private void Start()
    {
        audioManager.Play("ShieldActive");
    }
    public void Push()
    {
        if (stamina.EnoughStamina(pushCost))
        {
            List<Collider2D> colliders = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D().NoFilter();
            filter.SetLayerMask(pushLayers);
            Physics2D.OverlapCollider(pushCollider, filter, colliders);
            foreach (Collider2D collider2D in colliders)
            {
                if (collider2D.CompareTag("Player"))
                {
                    Vector2 dir = (Vector2)collider2D.GetComponent<Health>().playerCentre.position - (Vector2)player.position;
                    dir = dir.normalized;
                    collider2D.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushKnockback, ForceMode2D.Impulse);
                    collider2D.gameObject.GetComponent<Health>().Damage(0, 0, 0.5f);
                    audioManager.Play("Push");
                }
                else if (collider2D.CompareTag("Enemy"))
                {
                    Vector2 dir = (Vector2)collider2D.transform.position - (Vector2)player.position;
                    dir = dir.normalized;
                    collider2D.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * pushKnockback, ForceMode2D.Impulse);
                    audioManager.Play("Push");
                }
                else if (collider2D.CompareTag("Lever"))
                {
                    collider2D.GetComponent<Lever>().InteractLever();
                    audioManager.Play("Push2");
                }
                else if (collider2D.GetComponent<ReviveStation>())
                {
                    collider2D.GetComponent<ReviveStation>().ReviveGnome();
                    audioManager.Play("Push2");
                }
            }
            if (colliders.Count == 0)
                audioManager.Play("Cast");
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
            audioManager.Play("Block");
            stamina.SpendStamina(shieldCost);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y > transform.position.y)
                collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
