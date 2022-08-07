using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GnomeController : MonoBehaviour, ICharacters
{
    public float speed = 40f;
    private CharacterMovement characterMov;
    private Dash dash;
    private Animator animator;
    private SwordAttack attack;
    private Weapon weapon;
    private Stamina stamina;
    private WeaponMovement weaponMov;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool activeWeapon = false;
    private bool _stunned = false;

    private bool dead = false;


    PhotonView view;

    private void Awake()
    {
        characterMov = GetComponent<CharacterMovement>();
        dash = GetComponent<Dash>();
        animator = GetComponent<Animator>();
        attack = GetComponent<SwordAttack>();
        stamina = GetComponent<Stamina>();
        weaponMov = GetComponent<WeaponMovement>();
        weapon = GetComponent<Weapon>();
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (view.IsMine && !dead)
        {
            horizontalMove = Input.GetAxisRaw("GnomeHorizontal") * speed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (Input.GetButtonDown("GnomeJump"))
            {
                jump = true;
                animator.SetTrigger("Jumping");
                animator.SetBool("Falling", false);
            }
            if (Input.GetButtonDown("Attack"))
            {
                attack.Attack();
            }
            //if (Input.GetButtonDown("Shoot"))
            //{
            //    weapon.Fire();
            //}
            if(Input.GetAxis("GnomeHorizontal") != 0 && Input.GetButtonDown("Dash") && stamina.EnoughStamina(dash.staminaCost))
            {
                dash.DoDash(horizontalMove);
                stamina.SpendStamina(dash.staminaCost);
            }
            //if (Input.GetButtonDown("ToggleWeapon"))
            //    weaponMov.ToggleWeapon();
            if (Input.GetButton("WeaponHorizontal") || Input.GetAxisRaw("WeaponVertical") > 0)
            {
                if (activeWeapon)
                {
                    weapon.Fire();
                    return;
                }
                weaponMov.ToggleWeapon();
                activeWeapon = true;
                animator.SetBool("Weapon", true);
                //audio?
            }
            else
            {
                if (!activeWeapon)
                    return;
                weaponMov.ToggleWeapon();
                activeWeapon = false;
                animator.SetBool("Weapon", false);
            }
        }
    }
    private void FixedUpdate()
    {
        if (view.IsMine && !dash.isDashing && !_stunned)
        {
            characterMov.Move(horizontalMove * Time.fixedDeltaTime, false, jump, false);
            jump = false;
        }
    }
    public void Stunned(bool stun)
    {
        _stunned = stun;
    }
    public void DeathDisable()
    {
        dead = true;
        weaponMov.TurnOffWeapon();
    }
    public void ReviveEnable()
    {
        dead = false;
        weaponMov.ToggleWeapon();
    }
}
