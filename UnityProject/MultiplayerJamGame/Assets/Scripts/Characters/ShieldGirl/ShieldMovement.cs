using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    public Transform shield;

    private Animator animator;

    private float cooldown = 0.15f;
    private float _cooldown;
    private bool moved = false;

    private int xDir;
    private int yDir;

    private bool shieldActive = false;

    [SerializeField]private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private Transform upPos;
    [SerializeField] private Transform upLeftPos;
    [SerializeField] private Transform upRightPos;
    private void Start()
    {
        animator = GetComponent<Animator>();
        _cooldown = cooldown;
    }
    private void Update()
    {
        xDir = Mathf.RoundToInt(Input.GetAxisRaw("Debug Horizontal"));
        yDir = Mathf.RoundToInt(Input.GetAxisRaw("Debug Vertical"));
        animator.SetInteger("XDir", xDir);
        animator.SetInteger("YDir", yDir);
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
            if (xDir == 1 && yDir == 1)
            {
                shield.position = upRightPos.position;
                shield.rotation = upRightPos.rotation;
                moved = true;
            }
            else if (xDir == -1 && yDir == 1)
            {
                shield.position = upLeftPos.position;
                shield.rotation = upLeftPos.rotation;
                moved = true;
            }
            else if (xDir == 1 && yDir == 0)
            {
                shield.position = rightPos.position;
                shield.rotation = rightPos.rotation;
                moved = true;
            }
            else if (xDir == -1 && yDir == 0)
            {
                shield.position = leftPos.position;
                shield.rotation = leftPos.rotation;
                moved = true;
            }
            else if (yDir == 1)
            {
                shield.position = upPos.position;
                shield.rotation = upPos.rotation;
                moved = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Tocaste para pushear");
                shield.GetComponent<Shield>().Push();
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
