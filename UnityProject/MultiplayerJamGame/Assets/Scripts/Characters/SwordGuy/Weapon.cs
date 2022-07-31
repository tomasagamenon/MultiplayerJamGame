using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPref;
    public Transform shootingPoint;
    public int Damage;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Fire();
        }
    }
    public void Fire()
    {
        Instantiate(bulletPref, shootingPoint.position, shootingPoint.rotation);
    }
}
