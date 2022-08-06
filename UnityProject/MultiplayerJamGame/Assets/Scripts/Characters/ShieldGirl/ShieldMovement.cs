using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    public Transform shield;

    private Animator animator;
    private CharacterMovement characterMov;

    public enum AnimatorState { left, diagonalLeft, up, diagonalRight, right }
    private AnimatorState animatorState = AnimatorState.right;

    //private float cooldown = 0.15f;
    //private float _cooldown;
    //private bool moved = false;

    private int xDir;
    private int yDir;

    private bool shieldActive = false;

    [SerializeField]private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private Transform upPos;
    [SerializeField] private Transform upLeftPos;
    [SerializeField] private Transform upRightPos;

    private bool firstActive;
    private void Awake()
    {
        firstActive = true;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        characterMov = GetComponent<CharacterMovement>();
        //_cooldown = cooldown;
    }
    private void Update()
    {
        xDir = Mathf.RoundToInt(Input.GetAxisRaw("ShieldHorizontal"));
        yDir = Mathf.RoundToInt(Input.GetAxisRaw("ShieldVertical"));
        
        if(shieldActive)
        {
            if (xDir == 1 && yDir == 1)
            {
                shield.position = upRightPos.position;
                shield.rotation = upRightPos.rotation;
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.diagonalRight;
                else
                    animatorState = AnimatorState.diagonalLeft;
                //moved = true;
            }
            else if (xDir == -1 && yDir == 1)
            {
                shield.position = upLeftPos.position;
                shield.rotation = upLeftPos.rotation;
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.diagonalLeft;
                else
                    animatorState = AnimatorState.diagonalRight;
                //moved = true;
            }
            else if (xDir == 1 && yDir == 0)
            {
                shield.position = rightPos.position;
                shield.rotation = rightPos.rotation;
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.right;
                else
                    animatorState = AnimatorState.left;
                //moved = true;
            }
            else if (xDir == -1 && yDir == 0)
            {
                shield.position = leftPos.position;
                shield.rotation = leftPos.rotation;
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.left;
                else
                    animatorState = AnimatorState.right;
                //moved = true;
            }
            else if (yDir == 1)
            {
                shield.position = upPos.position;
                shield.rotation = upPos.rotation;
                animatorState = AnimatorState.up;
                //moved = true;
            }
            if (Input.GetButtonDown("Push"))
            {
                Debug.Log("Tocaste para pushear");
                shield.GetComponent<Shield>().Push();
            }
            animator.SetInteger("State", ((int)animatorState));
        }
    }
    public void ToggleShield()
    {
        shield.gameObject.SetActive(!shield.gameObject.activeSelf);
        shieldActive = !shieldActive;
        if (shieldActive && firstActive)
        {
            firstActive = false;
            shield.position = rightPos.position;
            shield.rotation = rightPos.rotation;
            animatorState = AnimatorState.right;
        }
        //code for shield activation/deactivation here
    }
    public void TurnOffShield()
    {
        shield.gameObject.SetActive(false);
        shieldActive = false;
    }
}
