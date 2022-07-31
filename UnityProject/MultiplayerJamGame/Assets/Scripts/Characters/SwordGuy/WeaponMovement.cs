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
                shootingPoint.position = upRightPos.position;
                shootingPoint.rotation = upRightPos.rotation;
            }
            else if (xDir == -1 && yDir == 1)
            {
                shootingPoint.position = upLeftPos.position;
                shootingPoint.rotation = upLeftPos.rotation;
            }
            else if (xDir == 1 && yDir == 0)
            {
                shootingPoint.position = rightPos.position;
                shootingPoint.rotation = rightPos.rotation;
            }
            else if (xDir == -1 && yDir == 0)
            {
                shootingPoint.position = leftPos.position;
                shootingPoint.rotation = leftPos.rotation;
            }
            else if (yDir == 1)
            {
                shootingPoint.position = upPos.position;
                shootingPoint.rotation = upPos.rotation;
            }
        }
    }
    public void ToggleWeapon()
    {
        weaponActive = !weaponActive;
        shootingPoint.gameObject.SetActive(!shootingPoint.gameObject.activeSelf);
    }
}
