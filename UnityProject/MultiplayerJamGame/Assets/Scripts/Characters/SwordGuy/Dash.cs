using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashDistance = 10f;
    public float dashTime = 0.4f;
    public float doubleTapMargin = 0.3f;
    public float downwardForce = -0.5f;
    public bool isDashing = false;
    public GameObject dashCollider;
    public Collider2D[] playerColliders;
    private float doubleTapTime;
    KeyCode lastKey;

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(doubleTapTime > Time.time && lastKey == KeyCode.A)
            {
                StartCoroutine(MakeDash(-1f));
            }
            else
            {
                doubleTapTime = Time.time + doubleTapMargin;
            }
            lastKey = KeyCode.A;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (doubleTapTime > Time.time && lastKey == KeyCode.D)
            {
                StartCoroutine(MakeDash(1f));
            }
            else
            {
                doubleTapTime = Time.time + doubleTapMargin;
            }
            lastKey = KeyCode.D;
        }
    }
    IEnumerator MakeDash(float direction)
    {
        isDashing = true;
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        dashCollider.SetActive(true);
        foreach (Collider2D item in playerColliders)
            item.isTrigger = true;
        rb.AddForce(new Vector2(direction * dashDistance, downwardForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        foreach (Collider2D item in playerColliders)
            item.isTrigger = false;
        dashCollider.SetActive(false);
        rb.gravityScale = gravity;
    }
}
