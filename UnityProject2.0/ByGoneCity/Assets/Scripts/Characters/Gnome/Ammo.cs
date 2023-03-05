using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int amount = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<Gun>())
        {
            if (collision.GetComponent<Gun>().AddAmmo(amount))
                SelfDestruct();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Gun>())
        {
            if (collision.gameObject.GetComponent<Gun>().AddAmmo(amount))
                SelfDestruct();
        }
    }
    public void SelfDestruct()
    {
        //anim or sound?
        Destroy(gameObject);
    }
}
