using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 7;
    private int actualHealth;

    private bool invulnerable = false;
    private float invulCooldown;
    private bool stunned = false;
    private float stunCooldown;

    private bool healing = false;

    private ICharacters controller;

    public Image bar;
    public Transform playerCentre;
    private Animator animator;
    private AudioManager audioManager;
    private CapsuleCollider2D capsule;
    private BoxCollider2D box;

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        controller = GetComponent<ICharacters>();
        actualHealth = maxHealth;
    }
    private void Update()
    {
        if (invulnerable)
        {
            if(Time.time >= invulCooldown)
            {
                DisableInvulnerability();
            }
        }
        if (stunned)
        {
            if(Time.time >= stunCooldown)
            {
                DisableStun();
            }
        }
    }
    public void Heal(int heal)
    {
        actualHealth = Mathf.Clamp(actualHealth + heal, 0, maxHealth);
        bar.fillAmount = Mathf.InverseLerp(0, maxHealth, actualHealth);
        if(actualHealth <= maxHealth)
            audioManager.Play("Healing");
        //if (actualHealth >= maxHealth)
        //{
        //    audioManager.StopPlaying("Healing");
        //    healing = false;
        //    return;
        //}
        //else if (!healing)
        //{
        //    audioManager.Play("Healing");
        //    healing = true;
        //    return;
        //}
        
    }
    #region Damage

    public void Damage(int damage)
    {
        DoDamage(damage);
    }
    public void Damage(int damage, float invulnerability)
    {
        DoDamage(damage);
        Invulnerability(invulnerability);
    }
    public void Damage(int damage, float invulnerability, float stun)
    {
        DoDamage(damage);
        Invulnerability(invulnerability);
        Stun(stun);
    }
    #endregion
    private void DoDamage(int damage)
    {
        if (!invulnerable)
        {
            actualHealth -= damage;
            audioManager.Play("Hit");
        }
        //sound?
        //animation?
        if(actualHealth <= 0)
        {
            Death();
        }
        bar.fillAmount = Mathf.InverseLerp(0, maxHealth, actualHealth);
    }
    public void MakeInvulnerable(float time)
    {
        Invulnerability(time);
    }
    private void Invulnerability(float time)
    {
        if (!invulnerable)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, 6, true);
            Physics2D.IgnoreLayerCollision(gameObject.layer, 10, true);
            invulCooldown = Time.time + time;
            invulnerable = true;
        }
    }
    private void DisableInvulnerability()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, 6, false);
        Physics2D.IgnoreLayerCollision(gameObject.layer, 10, false);
        invulnerable = false;
    }
    private void Stun(float time)
    {
        //hacer que se modifique la toma de inputs para que no se mueva el jugador, no se 
        // que otra cosa podria hacer
        if(!invulnerable && !stunned)
        {
            stunned = true;
            stunCooldown = Time.time + time;
            controller.Stunned(stunned);
        }
    }
    private void DisableStun()
    {
        //que se vuelva a habilitar todo
        stunned = false;
        controller.Stunned(stunned);
    }
    private void Death()
    {
        animator.SetBool("Dead", true);
        audioManager.Play("Death");
        controller.DeathDisable();
        capsule.enabled = false;
        box.enabled = false;
    }
    public void Revive(int life)
    {
        animator.SetBool("Dead", false);
        controller.ReviveEnable();
        Heal(life);
        audioManager.StopPlaying("Healing");
        audioManager.Play("Revive");
        capsule.enabled = true;
        box.enabled = true;
    }
}
