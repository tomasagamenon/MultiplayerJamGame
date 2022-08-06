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

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool _stunned = false;


    PhotonView view;

    private void Awake()
    {
        characterMov = GetComponent<CharacterMovement>();
        dash = GetComponent<Dash>();
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
}
