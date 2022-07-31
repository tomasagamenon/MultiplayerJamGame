using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public int totalLife = 100;
    private int actualLife;

    private void Start()
    {
        actualLife = totalLife;
    }
    public void TakeDamage(int damage)
    {
        actualLife -= damage;
        Debug.Log("Enemy life: " + actualLife);
        if (actualLife <= 0)
            Death();
    }
    private void Death()
    {
        transform.gameObject.SetActive(false);
        //death animation and disabling code and collider
    }
}
