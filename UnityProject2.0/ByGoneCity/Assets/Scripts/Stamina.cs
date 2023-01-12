using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Health
{
    protected float actualStamina;
    protected float recoverCooldown;

    new private void Start()
    {
        actualStamina = Data.maxStamina;
    }
    private void Update()
    {
        if (actualStamina < Data.maxStamina && Time.time >= recoverCooldown)
        {
            actualStamina = Mathf.Clamp(actualStamina + (Data.regeneration * Time.deltaTime), 0f, Data.maxStamina);
            //llenar barra
            if (actualStamina >= Data.maxStamina)
            {
                //audio de stamina llena
            }
        }
    }
    protected void SpendStamina(float amount)
    {
        actualStamina -= amount;
        recoverCooldown = Time.time + Data.recoverRate;
        //vaciar barra
        if (actualStamina <= 0)
        {
            //Audio de sin stamina
        }
    }
    protected bool EnoughStamina(float expected)
    {
        if (expected < actualStamina)
            return true;
        //audio sin stamina
        return false;
    }
}
