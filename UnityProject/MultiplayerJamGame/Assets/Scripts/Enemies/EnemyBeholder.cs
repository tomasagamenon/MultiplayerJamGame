using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeholder : EnemyMove
{
    public float distanceToSee;

    protected override void Start()
    {
        base.Start();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        attacking = false;
    }

    void Update()
    {
        foreach (Health player in FindObjectsOfType<Health>())
        {
            if (distanceToAttack < Vector2.Distance(transform.position, player.transform.position))
            {
                Attack(player.transform);
                attacking = true;
            }
            else attacking = false;
            if (!attacking)
            {
                if (distanceToSee < Vector2.Distance(transform.position, player.transform.position))
                {
                    var heading = player.transform.position - transform.position;
                    var distance = heading.magnitude;
                    var direction = heading / distance;
                    Move(direction.x, 1);
                }
            }

        }
    }

    private Vector3 direction;
    private Vector3 playerPos;

    protected override void Attack(Transform player)
    {
        if (!attacking)
            playerPos = player.position;
        var heading = playerPos - transform.position;
        var distance = heading.magnitude;
        direction = heading / distance;
        if (distance <= 0.5f )
        {
            Move(-direction.x, attackMultiplierSpeed);
            Move(-direction.y, attackMultiplierSpeed);
        }
        else
        {
            Move(direction.x, attackMultiplierSpeed);
            Move(direction.y, attackMultiplierSpeed);
        }
    }
}
