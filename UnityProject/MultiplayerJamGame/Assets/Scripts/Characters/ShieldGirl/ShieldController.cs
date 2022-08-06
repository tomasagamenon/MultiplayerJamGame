using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShieldController : MonoBehaviour, ICharacters
{
    public float speed = 40f;
    private CharacterMovement characterMov;
    private ShieldMovement shieldMov;
    private Animator animator;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool shield = false;
    private bool _stunned = false;

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
        if (view.IsMine)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetTrigger("Jumping");
                animator.SetBool("Falling", false);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow))
            {
                if (shield)
                    return;
                shieldMov.ToggleShield();
                shield = true;
                animator.SetBool("Shield", shield);
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
}
