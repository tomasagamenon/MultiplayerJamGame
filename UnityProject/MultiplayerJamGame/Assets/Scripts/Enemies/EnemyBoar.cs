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
                        randomDirection = Random.Range(-1, 0);
                    else randomDirection = Random.Range(0, 1);
                }
                move = randomDirection;
                _timeWalking = Random.Range(0.5f, maxTimeWalking);
                _currentTimeWalking = 0;
            }
            else _currentTimeWalking += Time.deltaTime;
            Move(move, 1);
            foreach(CharacterController player in FindObjectsOfType<CharacterController>())
            {
                if (distanceToAttack < Vector2.Distance(transform.position, player.transform.position))
                    foreach (EnemyBoar boar in herd)
                        boar.HerdAttack(player.transform);
            }
        }

    }

    public void HerdAttack(Transform player)
    {
        attacking = true;
        Attack(player);
    }

    protected override void Attack(Transform player)
    {
        var heading = player.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        Move(direction.x, attackMultiplierSpeed);
    }
}
