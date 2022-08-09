using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int maxHealth;
    protected int actualHealth;
    public int ActualHealth { get { return actualHealth; } set { actualHealth = value; CheckDeath(); } }

    protected virtual void Start()
    {
        actualHealth = maxHealth;
    }

    private void CheckDeath()
    {
        if (actualHealth <= 0)
            Death();
    }

    protected virtual void Death()
    {
        
    }
}
