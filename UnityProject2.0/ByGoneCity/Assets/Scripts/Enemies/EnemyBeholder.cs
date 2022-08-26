using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeholder : Attacking
{
    protected override void Start()
    {
        base.Start();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        attacking = false;
    }

    void Update()
    {
        Health playerTemp = null;
        foreach (Health player in FindObjectsOfType<Health>())
        {
            if (!player.GetComponent<EnemyMovement>())
            {
                if (playerTemp == null)
                    playerTemp = player;
                else if (Vector2.Distance(transform.position, player.transform.position) < Vector2.Distance(transform.position, playerTemp.transform.position))
                {
                    if (distanceToAttack > Vector2.Distance(transform.position, player.transform.position))
                    {
                        Attack(player.transform);
                    }
                    else attacking = false;
                }
                else
                {
                    if (distanceToAttack > Vector2.Distance(transform.position, playerTemp.transform.position))
                    {
                        Attack(player.transform);
                    }
                    else attacking = false;
                }
            }
        }
    }

    private Vector3 direction;
    private Vector3 playerPos;

    protected override void Attack(Transform player)
    {
        Debug.Log("attack");
        if (!attacking)
            playerPos = player.position;
        var heading = playerPos - transform.position;
        var distance = heading.magnitude;
        direction = heading / distance;
        if (distance <= 0.5f)
        {
            Move(-direction.x * Time.deltaTime, attackMultiplierSpeed);
            Move(-direction.y * Time.deltaTime, attackMultiplierSpeed);
        }
        else
        {
            Move(direction.x * Time.deltaTime, attackMultiplierSpeed);
            Move(direction.y * Time.deltaTime, attackMultiplierSpeed);
        }
    }
}
