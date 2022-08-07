using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShieldMovement : MonoBehaviourPunCallbacks
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

    public PhotonView photonView;

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
                photonView.RPC("RPC_ToggleShield", RpcTarget.All, new object[] { upRightPos.transform.position, upRightPos.transform.rotation, shield.gameObject.activeSelf });
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.diagonalRight;
                else
                    animatorState = AnimatorState.diagonalLeft;
                //moved = true;
            }
            else if (xDir == -1 && yDir == 1)
            {
                photonView.RPC("RPC_ToggleShield", RpcTarget.All, new object[] { upLeftPos.transform.position, upLeftPos.transform.rotation, shield.gameObject.activeSelf });
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.diagonalLeft;
                else
                    animatorState = AnimatorState.diagonalRight;
                //moved = true;
            }
            else if (xDir == 1 && yDir == 0)
            {
                photonView.RPC("RPC_ToggleShield", RpcTarget.All, new object[] { rightPos.transform.position, rightPos.transform.rotation, shield.gameObject.activeSelf });
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.right;
                else
                    animatorState = AnimatorState.left;
                //moved = true;
            }
            else if (xDir == -1 && yDir == 0)
            {
                photonView.RPC("RPC_ToggleShield", RpcTarget.All, new object[] { leftPos.transform.position, leftPos.transform.rotation, shield.gameObject.activeSelf });
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.left;
                else
                    animatorState = AnimatorState.right;
                //moved = true;
            }
            else if (yDir == 1)
            {
                photonView.RPC("RPC_ToggleShield", RpcTarget.All, new object[] { upPos.transform.position, upPos.transform.rotation, shield.gameObject.activeSelf });
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
        Debug.Log("a");
        photonView.RPC("RPC_ToggleShield", RpcTarget.All, new object[] {shield.transform.position, shield.transform.rotation, !shield.gameObject.activeSelf });
        shieldActive = !shieldActive;
        if (shieldActive && firstActive)
        {
            firstActive = false;
            photonView.RPC("RPC_ToggleShield", RpcTarget.All, new object[] { rightPos.transform.position, rightPos.transform.rotation, shield.gameObject.activeSelf });
            animatorState = AnimatorState.right;
        }
        //code for shield activation/deactivation here
    }

    [Photon.Pun.RPC]
    private void RPC_ToggleShield(Vector3 transform, Quaternion quaternion, bool active)
    {
        Debug.Log("AAA");
        shield.position = transform;
        shield.rotation = quaternion;


        shield.gameObject.SetActive(active);
    }
    public void TurnOffShield()
    {
        photonView.RPC("RPC_ToggleShield", RpcTarget.All, new object[] { shield.transform, false });
        shieldActive = false;
    }
}
