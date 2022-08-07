using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LaserPointing : MonoBehaviour
{
    public GameObject laserMedium;
    public SpriteRenderer medium;
    public GameObject laserPoint;
    public SpriteRenderer point;
    public LayerMask layer;
    public float raycastLenght = 15f;
    private Animator animator;

    public float stopCooldown = 1f;
    private float cooldown;
    private bool stopped = false;

    public bool turnedOff = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cooldown = stopCooldown;
        SetPosition();
    }
    private void Update()
    {
        if (stopped && !turnedOff)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                SetPosition();
                cooldown = stopCooldown;
            }
        }
    }
    private void FixedUpdate()
    {
        if(!stopped && !turnedOff)
            SetPosition();
    }
    private void SetPosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -transform.up, raycastLenght, layer);
        if(hit.point != (Vector2)laserPoint.transform.position)
        {
            laserPoint.transform.position = hit.point;
            Vector2 place = new Vector2(laserPoint.transform.position.x, laserPoint.transform.position.y);
            float scaleY = Vector2.Distance(transform.position, laserPoint.transform.position);
            if (scaleY < 0.05)
            {
                LaserStop();
                stopped = true;
            }
            else if(stopped)
            {
                stopped = false;
                LaserStart();
            }
            laserMedium.transform.position = place;
            medium.drawMode = SpriteDrawMode.Tiled;
            medium.size = new Vector2(1, scaleY);
            laserMedium.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, scaleY);
            Vector2 offset = new Vector2(0, scaleY / 2);
            laserMedium.GetComponent<BoxCollider2D>().offset = offset;
        }
    }
    public void LaserStart()
    {
        animator.SetBool("Active", true);
        GetComponent<AudioManager>().Play("Activate");
        //audio
    }
    public void ActivateLaser()
    {
        animator.SetBool("Active", true);
        laserMedium.SetActive(true);
        laserPoint.SetActive(true);
        //audio loop
    }
    public void LaserStop()
    {
        animator.SetBool("Active", false);
        GetComponent<AudioManager>().Play("Deactivate");
        laserPoint.SetActive(false);
        laserMedium.SetActive(false);
        //audio
    }
}
