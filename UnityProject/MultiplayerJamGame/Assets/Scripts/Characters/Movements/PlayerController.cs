using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 40f;
    private CharacterMovement characterMov;
    private ShieldMovement shieldMov;
    private Animator animator;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool shield = false;

    private void Awake()
    {
        characterMov = GetComponent<CharacterMovement>();
        shieldMov = GetComponent<ShieldMovement>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetTrigger("Jumping");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shieldMov.ToggleShield();
            shield = !shield;
            animator.SetBool("Shield", shield);
        }
    }
    private void FixedUpdate()
    {
        characterMov.Move(horizontalMove * Time.fixedDeltaTime, false, jump, shield);
        jump = false;
    }
}
