using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isToggle = true;
    private bool isActive = false;
    public GameObject linkedObject;
    private IActivable activable;

    private Animator animator;
    private void Awake()
    {
        activable = linkedObject.GetComponent<IActivable>();
        animator = GetComponent<Animator>();
    }
    public void InteractLever()
    {
        if(isActive && isToggle)
        {
            LeverOff();
        }
        else
        {
            LeverOn();
        }
    }
    private void LeverOn()
    {
        isActive = true;
        animator.SetBool("On", true);
        activable.TurnOn();
        //sound?
    }
    private void LeverOff()
    {
        isActive = false;
        animator.SetBool("On", false);
        activable.TurnOff();
        //sound?
    }
}
