using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField]
    protected CharacterData Data;
    [SerializeField]
    protected Animator Animator;
    protected int actualHealth;
    [SerializeField]
    private int iFramesSeconds;
    private bool iFrames;
    public int ActualHealth { get { return actualHealth; } set { if (iFrames && actualHealth < value) return;
            actualHealth = value; StartCoroutine(IFrames()); CheckDeath(); } }

    protected virtual void Start()
    {
        actualHealth = Data.maxHealth;
    }

    private void CheckDeath()
    {
        if (actualHealth <= 0)
            Death();
    }

    protected virtual void Death()
    {
        
    }

    IEnumerator IFrames()
    {
        iFrames = true;
        yield return new WaitForSeconds(iFramesSeconds);
        iFrames = false;
    }
}
