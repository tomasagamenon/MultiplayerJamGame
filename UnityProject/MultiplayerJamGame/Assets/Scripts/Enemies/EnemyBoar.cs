using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoar : EnemyMove
{
    public EnemyHerd myHerd;
    public float distanceOfHerd;
    public List<EnemyBoar> herd;
    public float maxTimeWalking;
    private float _timeWalking;
    private float _currentTimeWalking;
    private int move;
    private bool lookingDeath;

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        _timeWalking = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            if (_currentTimeWalking >= _timeWalking)
            {
                int randomDirection;
                if (!lookingDeath && distanceOfHerd < Vector2.Distance(transform.position, myHerd.transform.position))
                    randomDirection = Random.Range(-1, 1);
                else
                {
                    if (m_FacingRight)
                        randomDirection = -1;
                    else randomDirection = 1;
                }
                move = randomDirection;
                _timeWalking = Random.Range(0.5f, maxTimeWalking);
                _currentTimeWalking = 0;
            }
            else _currentTimeWalking += Time.deltaTime;
            Move(move, 1);
        }
        Health playerTemp = null;
        foreach (Health player in FindObjectsOfType<Health>())
        {
            if (playerTemp == null)
                playerTemp = player;
            else if (Vector2.Distance(transform.position, player.transform.position) < Vector2.Distance(transform.position, playerTemp.transform.position))
            {
                if (distanceToAttack > Vector2.Distance(transform.position, player.transform.position))
                {
                    foreach (EnemyBoar boar in herd)
                        boar.HerdAttack(player.transform);
                }
                else attacking = false;
            } else
            {
                if (distanceToAttack > Vector2.Distance(transform.position, playerTemp.transform.position))
                {
                    foreach (EnemyBoar boar in herd)
                        boar.HerdAttack(playerTemp.transform);
                }
                else attacking = false;
            }
        }

    }

    public void HerdAttack(Transform player)
    {
        if (distanceToAttack > Vector2.Distance(transform.position, player.transform.position))
        {
            Debug.Log("ataco");
            attacking = true;
            Attack(player);
        }
        else attacking = false;
    }

    protected override void Attack(Transform player)
    {
        var heading = player.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        Move(direction.x, attackMultiplierSpeed);
    }
}
