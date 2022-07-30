using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 40f;
    private CharacterMovement characterMov;
    private ShieldMovement shieldMov;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool shield = false;
    private void Awake()
    {
        characterMov = GetComponent<CharacterMovement>();
        shieldMov = GetComponent<ShieldMovement>();
    }
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetButtonDown("Jump"))
            jump = true;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shieldMov.ToggleShield();
            shield = !shield;
        }
    }
    private void FixedUpdate()
    {
        characterMov.Move(horizontalMove * Time.fixedDeltaTime, false, jump, shield);
        jump = false;
    }
}
