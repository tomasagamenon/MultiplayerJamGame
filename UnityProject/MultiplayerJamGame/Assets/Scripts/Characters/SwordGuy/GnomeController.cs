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
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (view.IsMine && !dead)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetTrigger("Jumping");
                animator.SetBool("Falling", false);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                attack.Attack();
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                weapon.Fire();
            }
            if(Input.GetAxis("Horizontal") != 0 && Input.GetKeyDown(KeyCode.LeftShift) && stamina.EnoughStamina(dash.staminaCost))
            {
                dash.DoDash(horizontalMove);
                stamina.SpendStamina(dash.staminaCost);
            }
            if (Input.GetKeyDown(KeyCode.K))
                weaponMov.ToggleWeapon();
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
