using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    public Transform shield;

    private float cooldown = 0.15f;
    private float _cooldown;
    private bool moved = false;

    private bool shieldActive = false;

    [SerializeField]private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private Transform upPos;
    [SerializeField] private Transform upLeftPos;
    [SerializeField] private Transform upRightPos;
    private void Start()
    {
        _cooldown = cooldown;
    }
    private void Update()
    {
        if (moved)
        {
            _cooldown -= Time.deltaTime;
            if (_cooldown < 0)
            {
                moved = false;
                _cooldown = cooldown;
            }
        }
        else if(shieldActive)
        {
            if (Input.GetButton("Right") && Input.GetButton("Up"))
            {
                shield.position = upRightPos.position;
                shield.rotation = upRightPos.rotation;
                moved = true;
            }
            else if (Input.GetButton("Left") && Input.GetButton("Up"))
            {
                shield.position = upLeftPos.position;
                shield.rotation = upLeftPos.rotation;
                moved = true;
            }
            else if (Input.GetButton("Right") /*|| Input.GetButtonUp("Up") && Input.GetButton("Right")*/)
            {
                shield.position = rightPos.position;
                shield.rotation = rightPos.rotation;
                moved = true;
            }
            else if (Input.GetButton("Left") /*|| Input.GetButtonUp("Up") && Input.GetButton("Left")*/)
            {
                shield.position = leftPos.position;
                shield.rotation = leftPos.rotation;
                moved = true;
            }
            else if (Input.GetButton("Up") /*|| Input.GetButtonUp("Right") && Input.GetButton("Up")*/
                /*|| Input.GetButtonUp("Left") && Input.GetButton("Up")*/)
            {
                shield.position = upPos.position;
                shield.rotation = upPos.rotation;
                moved = true;
            }
        }
    }
    public void ToggleShield()
    {
        shield.gameObject.SetActive(!shield.gameObject.activeSelf);
        shieldActive = !shieldActive;
        //code for shield activation/deactivation here
    }
}
