using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveStation : MonoBehaviour
{
    public GameObject orc;
    public GameObject gnome;
    public float healingRadius = 5f;
    public float orcHealingSpeed = 1f;
    public float gnomeHealingSpeed = 1f;
    private float orcCooldown = 0f;
    private float gnomeCooldown = 0f;
    public LayerMask playersLayers;
    private bool revived = false;
    public int orcFirstRevive = 7;
    public int orcSecondRevive = 4;
    public int gnomeFirstRevive = 3;
    public int gnomeSecondRevive = 2;
    public Transform respawn;


    private void Awake()
    {

    }
    private void Start()
    {
        orc = FindObjectOfType<OrcController>().gameObject;
        gnome = FindObjectOfType<GnomeController>().gameObject;
    }
    private void Update()
    {
        if (!revived)
        {
            CheckPlayers();
        }
    }
    private void CheckPlayers()
    {
        if(Vector2.Distance((Vector2)transform.position, (Vector2)gnome.transform.position) <= healingRadius && gnome.activeInHierarchy)
        {
            if(Time.time >= gnomeCooldown)
            {
                gnome.GetComponent<Health>().Heal(1);
                gnomeCooldown = Time.time + gnomeHealingSpeed;
            }
        }
        if(Vector2.Distance((Vector2)transform.position, (Vector2)orc.transform.position) <= healingRadius && orc.activeInHierarchy)
        {
            if (Time.time >= orcCooldown)
            {
                orc.GetComponent<Health>().Heal(1);
                orcCooldown = Time.time + orcHealingSpeed;
            }
        }
    }
    public void ReviveOrc()
    {
        orc.transform.position = respawn.position;
        if (!revived)
        {
            orc.GetComponent<Health>().Revive(orcFirstRevive);
            revived = true;
        }
        else
        {
            orc.GetComponent<Health>().Revive(orcSecondRevive);
        }
    }
    public void ReviveGnome()
    {
        gnome.transform.position = respawn.position;
        if (!revived)
        {
            gnome.GetComponent<Health>().Revive(gnomeFirstRevive);
        }
        else
        {
            gnome.GetComponent<Health>().Revive(gnomeSecondRevive);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healingRadius);
    }
}
