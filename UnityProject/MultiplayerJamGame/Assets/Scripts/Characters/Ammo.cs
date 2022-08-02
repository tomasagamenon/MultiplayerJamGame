using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int amount = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.GetComponent<Weapon>())
        {
            if (collision.GetComponent<Weapon>().AddAmmo(amount))
                SelfDestruct();
        }
    }
    public void SelfDestruct()
    {
        //anim or sound?
        Destroy(gameObject);
    }
}
