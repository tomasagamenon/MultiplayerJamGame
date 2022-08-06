using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveStation : MonoBehaviour
{
    public float healingRadius = 5f;
    public float healingSpeed = 1f;
    private float healingCooldown = 0f;
    public LayerMask playersLayers;
    private bool revived = false;

    private void Update()
    {
        if (!revived)
        {
            CheckPlayers();
        }
    }
    private void CheckPlayers()
    {
        bool orc = false;
        bool gnome = false;
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, healingRadius, playersLayers);
        foreach (Collider2D pj in players)
        {
            if(pj.gameObject.layer == 9 && !orc)
            {
                orc = true;
            }else if(pj.gameObject.layer == 8 && !gnome)
            {
                if(Time.time >= healingCooldown)
                {
                    pj.GetComponent<Health>().Heal(1);
                    healingCooldown = Time.time + healingSpeed;
                }
                gnome = true;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healingRadius);
    }
}
