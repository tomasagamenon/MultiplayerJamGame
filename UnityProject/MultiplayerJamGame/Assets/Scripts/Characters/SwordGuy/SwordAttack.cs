using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int attackDamage = 25;
    public float attackRange = 0.4f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRate = 4f;
    public float attackMargin = 0.3f;
    private float attackCooldown = 0f;

    private bool isAttacking = false;
    private bool combo = false;
    private float lastAttack = 0f;

    void Update()
    {
        if (lastAttack <= Time.time)
        {
            EndAttack();
        }
        //if (Time.time >= attackCooldown)
        //{
        //    if (Input.GetKeyDown(KeyCode.F))
        //    {
        //        Attack();
        //        attackCooldown = Time.time + 1f / attackRate;
        //    }
        //}
    }
    public void Attack()
    {
        if(Time.time >= attackCooldown || combo)
        {
            if(lastAttack > Time.time && isAttacking)
            {
                GetComponent<Animator>().SetBool("SecondAttack", true);
                GetComponent<AudioManager>().Play("Punch4");
                //isAttacking = false;
            }
            else if (!isAttacking)
            {
                GetComponent<Animator>().SetBool("Attacking", true);
                GetComponent<Animator>().SetTrigger("FirstAttack");
                GetComponent<AudioManager>().Play("Punch3");
                isAttacking = true;
                combo = true;
                lastAttack = Time.time + attackMargin;
            }
            attackCooldown = Time.time + 1f / attackRate;
        }
    }
    public void EndAttack()
    {
        GetComponent<Animator>().SetBool("Attacking", false);
        GetComponent<Animator>().SetBool("SecondAttack", false);
        isAttacking = false;
    }
    public void UnableCombo()
    {
        combo = false;
        //EndAttack();
    }
    public void DoDamage()
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D impact in colliders2D)
        {
            if (impact.CompareTag("Enemy"))
            {
                Debug.Log("You hit " + impact.name);
                impact.GetComponent<EnemyLife>().TakeDamage(attackDamage);
                if(combo)
                    GetComponent<AudioManager>().Play("PunchHit3");
                else
                    GetComponent<AudioManager>().Play("PunchHit2");
            } else if (impact.CompareTag("Lever"))
            {
                GetComponent<AudioManager>().Play("PunchHit1");
                impact.GetComponent<Lever>().InteractLever();
            }
            else if (impact.GetComponent<ReviveStation>())
            {
                GetComponent<AudioManager>().Play("PunchHit1");
                impact.GetComponent<ReviveStation>().ReviveOrc();
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
