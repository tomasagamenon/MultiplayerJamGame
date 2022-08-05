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

    private void Awake()
    {
        activable = linkedObject.GetComponent<IActivable>();
        sr = GetComponent<SpriteRenderer>();
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
        if (collision.gameObject.CompareTag("Player") || collision.CompareTag("Cube"))
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
        if ((collision.gameObject.CompareTag("Player") || collision.CompareTag("Cube")) && (!isToggle))
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
        sr.color = Color.red;
    }
    private void ButtonOn()
    {
        isActive = true;
        activable.TurnOn();
        sr.color = Color.green;
    }
}
