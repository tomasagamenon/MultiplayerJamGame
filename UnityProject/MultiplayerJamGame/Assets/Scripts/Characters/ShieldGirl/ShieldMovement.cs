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
                GetComponent<PhotonView>().RPC("RPC_ToggleShield", RpcTarget.All, new object[] { upRightPos.transform, shield.gameObject.activeSelf });
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.diagonalRight;
                else
                    animatorState = AnimatorState.diagonalLeft;
                //moved = true;
            }
            else if (xDir == -1 && yDir == 1)
            {
                GetComponent<PhotonView>().RPC("RPC_ToggleShield", RpcTarget.All, new object[] { upLeftPos.transform, shield.gameObject.activeSelf });
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.diagonalLeft;
                else
                    animatorState = AnimatorState.diagonalRight;
                //moved = true;
            }
            else if (xDir == 1 && yDir == 0)
            {
                GetComponent<PhotonView>().RPC("RPC_ToggleShield", RpcTarget.All, new object[] { rightPos.transform, shield.gameObject.activeSelf });
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.right;
                else
                    animatorState = AnimatorState.left;
                //moved = true;
            }
            else if (xDir == -1 && yDir == 0)
            {
                GetComponent<PhotonView>().RPC("RPC_ToggleShield", RpcTarget.All, new object[] { leftPos.transform, shield.gameObject.activeSelf });
                if (characterMov.m_FacingRight)
                    animatorState = AnimatorState.left;
                else
                    animatorState = AnimatorState.right;
                //moved = true;
            }
            else if (yDir == 1)
            {
                GetComponent<PhotonView>().RPC("RPC_ToggleShield", RpcTarget.All, new object[] { upPos.transform, shield.gameObject.activeSelf });
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
        GetComponent<PhotonView>().RPC("RPC_ToggleShield", RpcTarget.All, new object[] {shield.transform, !shield.gameObject.activeSelf });
        shieldActive = !shieldActive;
        if (shieldActive && firstActive)
        {
            firstActive = false;
            GetComponent<PhotonView>().RPC("RPC_ToggleShield", RpcTarget.All, new object[] { rightPos.transform, shield.gameObject.activeSelf });
            animatorState = AnimatorState.right;
        }
        //code for shield activation/deactivation here
    }

    [Photon.Pun.RPC]
    private void RPC_ToggleShield(Transform transform, bool active)
    {
        shield.position = transform.position;
        shield.rotation = transform.rotation;


        shield.gameObject.SetActive(active);
    }
    public void TurnOffShield()
    {
        GetComponent<PhotonView>().RPC("RPC_ToggleShield", RpcTarget.All, new object[] { shield.transform, false });
        shieldActive = false;
    }
}
