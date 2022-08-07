using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    public Transform shootingPoint;

    private bool weaponActive = false;
    private int xDir;
    private int yDir;

    private Animator animator;
    private CharacterMovement characterMov;
    public enum AnimatorState { left, diagonalLeft, up, diagonalRight, right }
    private AnimatorState animatorState = AnimatorState.right;

    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private Transform upPos;
    [SerializeField] private Transform upLeftPos;
    [SerializeField] private Transform upRightPos;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterMov = GetComponent<CharacterMovement>();
    }
    void Update()
    {
        xDir = Mathf.RoundToInt(Input.GetAxisRaw("WeaponHorizontal"));
        yDir = Mathf.RoundToInt(Input.GetAxisRaw("WeaponVertical"));
        
        if (weaponActive)
        {
            if (xDir == 1 && yDir == 1)
            {
                shootingPoint.SetPositionAndRotation(upRightPos.position, upRightPos.rotation);
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.diagonalRight;
                else
                    animatorState = AnimatorState.diagonalLeft;
            }
            else if (xDir == -1 && yDir == 1)
            {
                shootingPoint.SetPositionAndRotation(upLeftPos.position, upLeftPos.rotation);
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.diagonalLeft;
                else
                    animatorState = AnimatorState.diagonalRight;
            }
            else if (xDir == 1 && yDir == 0)
            {
                shootingPoint.SetPositionAndRotation(rightPos.position, rightPos.rotation);
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.right;
                else
                    animatorState = AnimatorState.left;
            }
            else if (xDir == -1 && yDir == 0)
            {
                shootingPoint.SetPositionAndRotation(leftPos.position, leftPos.rotation);
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.left;
                else
                    animatorState = AnimatorState.right;
            }
            else if (yDir == 1)
            {
                shootingPoint.SetPositionAndRotation(upPos.position, upPos.rotation);
                animatorState = AnimatorState.up;
            }
            animator.SetInteger("State", (int)animatorState);
        }
    }
    public void ToggleWeapon()
    {
        weaponActive = !weaponActive;
        shootingPoint.gameObject.SetActive(!shootingPoint.gameObject.activeSelf);
    }
    public void TurnOffWeapon()
    {
        weaponActive = false;
        shootingPoint.gameObject.SetActive(false);
    }
}
