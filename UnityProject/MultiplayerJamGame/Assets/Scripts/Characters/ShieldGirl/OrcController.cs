using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OrcController : MonoBehaviour, ICharacters
{
    public float speed = 40f;
    private CharacterMovement characterMov;
    private ShieldMovement shieldMov;
    private Animator animator;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool shield = false;
    private bool _stunned = false;

    private bool dead = false;

    PhotonView view;

    private void Awake()
    {
        characterMov = GetComponent<CharacterMovement>();
        shieldMov = GetComponent<ShieldMovement>();
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (view.IsMine && !dead)
        {
            horizontalMove = Input.GetAxisRaw("OrcHorizontal") * speed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (Input.GetButtonDown("OrcJump"))
            {
                jump = true;
                animator.SetTrigger("Jumping");
                animator.SetBool("Falling", false);
            }
            if (Input.GetButton("ShieldHorizontal") || Input.GetAxisRaw("ShieldVertical") > 0)
            {
                if (shield)
                    return;
                shieldMov.ToggleShield();
                shield = true;
                animator.SetBool("Shield", shield);
                GetComponent<AudioManager>().Play("Shield");
            }
            else
            {
                if (!shield)
                    return;
                shieldMov.ToggleShield();
                shield = false;
                animator.SetBool("Shield", shield);
            }
        }
    }
    private void FixedUpdate()
    {
        if (view.IsMine && !_stunned)
        {
            characterMov.Move(horizontalMove * Time.fixedDeltaTime, false, jump, shield);
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
        shieldMov.TurnOffShield();
    }
    public void ReviveEnable()
    {
        dead = false;
        shieldMov.ToggleShield();
    }
}
