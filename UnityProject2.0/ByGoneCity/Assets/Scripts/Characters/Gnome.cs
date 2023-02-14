using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gnome : Gun
{
    private bool attacking;
    private int combo;
    [SerializeField]
    private Transform attackPoint;
    public void OnPunch(InputAction.CallbackContext input)
    {
        if (input.performed && !attacking)
            Combo();
    }
    public void Punch()
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(attackPoint.position, Data.attackArea, Data.attackLayers);
        foreach (Collider2D collider in collider2D)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Health>().ActualHealth -= Data.attackDamage;
                //Calcular dirección del golpe
                Vector2 direction = collider.transform.position - transform.position;
                collider.GetComponent<Rigidbody2D>().AddForce(direction * Data.attackKnockback, ForceMode2D.Impulse);
                //audio manager ejecutar "punchhit"
            }
            else if (collider.CompareTag("Lever"))
            {

            }
            else if (collider.CompareTag("ReviveStation"))
            {

            }
        }
    }
    public void Combo()
    {
        attacking = true;
        isSlowed = true;
        Animator.SetTrigger("Punch" + combo);
        //audio manager ejecutar "punch" + combo
    }
    public void StartCombo()
    {
        attacking = false;
        if(combo < Data.attackCombo)
        {
            combo++;
        }
    }
    public void FinishCombo()
    {
        attacking = false;
        isSlowed = false;
        combo = 0;
    }
    public void Turning()
    {
        Debug.Log("se giro el golpe");
        attackPoint.localPosition = new Vector2(-attackPoint.localPosition.x, attackPoint.localPosition.y);
    }
    new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, Data.attackArea);
    }
}
