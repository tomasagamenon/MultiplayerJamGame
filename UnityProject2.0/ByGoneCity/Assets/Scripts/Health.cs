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
    private int _iFramesSeconds;
    private bool isInvulnerable;
    public int ActualHealth { get { return actualHealth; } set { if (isInvulnerable && actualHealth < value) return;
            actualHealth = value; StartIFrames(_iFramesSeconds); CheckDeath(); } }

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
    protected void StartIFrames(float iFramesSeconds)
    {
        StartCoroutine(IFrames(iFramesSeconds));
    }
    IEnumerator IFrames(float iFramesSeconds)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(iFramesSeconds);
        isInvulnerable = false;
    }
}
