using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GnomeController : MonoBehaviour
{
    public float speed = 40f;
    private CharacterMovement characterMov;
    private Dash dash;
    private Animator animator;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool shield = false;

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
            //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                //animator.SetTrigger("Jumping");
                //animator.SetBool("Falling", false);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {

            }
        }
    }
    private void FixedUpdate()
    {
        if (view.IsMine && !dash.isDashing)
        {
            characterMov.Move(horizontalMove * Time.fixedDeltaTime, false, jump, shield);
            jump = false;
        }
    }
}
