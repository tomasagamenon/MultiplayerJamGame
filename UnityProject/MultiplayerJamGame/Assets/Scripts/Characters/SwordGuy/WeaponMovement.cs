using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    public Transform shootingPoint;

    private bool weaponActive = false;
    private int xDir;
    private int yDir;

    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private Transform upPos;
    [SerializeField] private Transform upLeftPos;
    [SerializeField] private Transform upRightPos;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            ToggleWeapon();

        xDir = Mathf.RoundToInt(Input.GetAxisRaw("Debug Horizontal"));
        yDir = Mathf.RoundToInt(Input.GetAxisRaw("Debug Vertical"));
        
        if (weaponActive)
        {
            if (xDir == 1 && yDir == 1)
            {
                shootingPoint.SetPositionAndRotation(upRightPos.position, upRightPos.rotation);
            }
            else if (xDir == -1 && yDir == 1)
            {
                shootingPoint.SetPositionAndRotation(upLeftPos.position, upLeftPos.rotation);
            }
            else if (xDir == 1 && yDir == 0)
            {
                shootingPoint.SetPositionAndRotation(rightPos.position, rightPos.rotation);
            }
            else if (xDir == -1 && yDir == 0)
            {
                shootingPoint.SetPositionAndRotation(leftPos.position, leftPos.rotation);
            }
            else if (yDir == 1)
            {
                shootingPoint.SetPositionAndRotation(upPos.position, upPos.rotation);
            }
        }
    }
    public void ToggleWeapon()
    {
        weaponActive = !weaponActive;
        shootingPoint.gameObject.SetActive(!shootingPoint.gameObject.activeSelf);
    }
}
