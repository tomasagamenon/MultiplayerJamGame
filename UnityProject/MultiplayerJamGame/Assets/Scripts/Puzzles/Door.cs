using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IActivable
{
    public int inputCounter = 1;
    private bool isClosed = true;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        inputCounter--;
        if(inputCounter <= 0)
        {
            animator.SetBool("Open", true);
            //sound?
        }
    }
    public void CloseDoor()
    {
        inputCounter++;
        if(inputCounter > 0)
        {
            animator.SetBool("Open", false);
            //sound?
        }
    }
    public void ToggleDoor()
    {
        if (isClosed)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }
    public void ToggleObject()
    {
        ToggleDoor();
    }
    public void TurnOn()
    {
        OpenDoor();
    }
    public void TurnOff()
    {
        CloseDoor();
    }
}
