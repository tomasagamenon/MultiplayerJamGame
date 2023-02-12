using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Gun : Movement
{
    [SerializeField] private Transform gunPoint;
    [SerializeField] private Transform[] gunDirections = new Transform[5];
    public Vector2 direction;
    public bool drawnWeapon = true;

    public void OnAim(InputAction.CallbackContext input)
    {
        direction = input.ReadValue<Vector2>();
        //Debug.Log("Aim value= " + direction);
    }
    public void OnShoot(InputAction.CallbackContext input)
    {

    }
    private void Update()
    {
        RotateGun();
    }
    private void RotateGun()
    {
        Vector2 newPosition = gunDirections[4].position;
        //Animator.SetFloat("wDirectionX", direction.x);
        //Animator.SetFloat("wDirectionY", direction.y);
        if (!drawnWeapon)
        {
            Debug.Log("no esta el arma");
            return;
        }
        if (direction.x > 0.1f)
        {
            if (direction.y > 0.1f)
            {
                newPosition = gunDirections[3].position;
                //apunta en diagonal derecha
            }
            else
            {
                newPosition = gunDirections[4].position;
                //apunta a la derecha
            }
        }
        else if (direction.x < -0.1f)
        {
            if (direction.y > 0.1f)
            {
                newPosition = gunDirections[1].position;
                //apunta en diagonal izquierda
            }
            else
            {
                newPosition = gunDirections[0].position;
                //apunta a la izquierda
            }
        }
        else if (direction.y > 0.1f)
        {
            newPosition = gunDirections[2].position;
            //apunta hacia arriba
        }
        if ((Vector2)gunPoint.position == newPosition)
        {
            Debug.Log("La posicion es la misma");
            return;
        }
        gunPoint.position = newPosition;
    }
}
