using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private CharacterData Data;
    public Orc orc;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && !orc.EnoughStamina(Data.shieldCost))
        {
            //hacer daño, empujar al PERSONAJE
            //audio de falta stamina
            return;
        }
        else
        {
            orc.SpendStamina(Data.shieldCost);
            Vector2 direction = (Vector2)collision.transform.position - collision.GetContact(0).point;
            direction = direction.normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * Data.shieldKnockback, ForceMode2D.Impulse);
            //ejecutar audio de bloqueo
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            //codigo para que se pegue al escudo
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //codigo para que se despegue del escudo
        }
    }
}
