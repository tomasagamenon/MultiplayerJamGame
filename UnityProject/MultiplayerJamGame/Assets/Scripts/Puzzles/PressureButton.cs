using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    public GameObject linkedObject;
    private IActivable activable;
    public bool isToggle = false;
    public bool isTimed = false;
    private bool isActive = false;

    public float time;
    private float cooldown;
    private SpriteRenderer sr;
    private Animator animator;
    private AudioManager audioManager;

    private void Awake()
    {
        activable = linkedObject.GetComponent<IActivable>();
        sr = GetComponent<SpriteRenderer>();
        audioManager = GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                ButtonOff();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(isToggle && isActive)
            {
                ButtonOff();
            }
            else
            {
                ButtonOn();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player")) && (!isToggle))
        {
            if (isTimed)
            {
                cooldown = time;
            }
            else
            {
                ButtonOff();
            }
        }
    }
    private void ButtonOff()
    {
        isActive = false;
        activable.TurnOff();
        audioManager.Play("Unpress");
        animator.SetBool("Pressed", false);
    }
    private void ButtonOn()
    {
        isActive = true;
        activable.TurnOn();
        audioManager.Play("Press");
        animator.SetBool("Pressed", true);
    }
}
