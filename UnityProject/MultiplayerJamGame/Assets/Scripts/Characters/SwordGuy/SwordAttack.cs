using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int attackDamage = 25;
    public float attackRange = 0.4f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRate = 3f;
    private float attackCooldown = 0f;

    void Update()
    {
        if(Time.time >= attackCooldown)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
                attackCooldown = Time.time + 1f / attackRate;
            }
        }
    }
    private void Attack()
    {
        //call animation
        DoDamage();
    }
    //later call this at the right time in the animation
    public void DoDamage()
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in colliders2D)
        {
            Debug.Log("You hit " + enemy.name);
            enemy.GetComponent<EnemyLife>().TakeDamage(attackDamage);
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
