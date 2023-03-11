using UnityEngine;

public class Stamina : Health
{
    protected float actualStamina;
    protected float recoverCooldown;

    new protected virtual void Start()
    {
        Debug.Log("Start stamina");
        base.Start();
        actualStamina = Data.maxStamina;
    }
    protected virtual void Update()
    {
        //Debug.Log("Update Stamina");
        if (actualStamina < Data.maxStamina && Time.time >= recoverCooldown)
        {
            actualStamina = Mathf.Clamp(actualStamina + (Data.regeneration * Time.deltaTime), 0f, Data.maxStamina);
            //llenar barra
            if (actualStamina >= Data.maxStamina)
            {
                audioManager.Play("StaminaRecharge");
                //audio de stamina llena
            }
        }
    }
    public void SpendStamina(float amount)
    {
        actualStamina -= amount;
        recoverCooldown = Time.time + Data.recoverRate;
        //vaciar barra
        if (actualStamina <= 0)
        {
            audioManager.Play("StaminaDepleted");
            //Audio de sin stamina
        }
    }
    public bool EnoughStamina(float expected)
    {
        if (expected < actualStamina)
            return true;
        audioManager.Play("OutOfStamina");
        //audio sin stamina
        return false;
    }
}
