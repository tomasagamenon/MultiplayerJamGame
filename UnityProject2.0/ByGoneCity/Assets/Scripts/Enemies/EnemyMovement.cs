using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Health
{
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    public Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    protected bool m_FacingRight = true;
    public float walkModifier;
    public float attackMultiplierSpeed;

    protected override void Start()
    {
        base.Start();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    protected void Move(float move, float attackMovement)
    {
        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(move));
        move *= walkModifier * attackMovement;
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
