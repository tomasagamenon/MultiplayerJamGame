using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float maxStamina = 100;
    private float actualStamina;
    public float regeneration = 10f;
    public float recoverRate = 1f;
    private float recoverCooldown = 0f;

    public Image bar;

    private void Awake()
    {
        actualStamina = maxStamina;
    }
    private void Update()
    {
        if(actualStamina < maxStamina && Time.time >= recoverCooldown)
        {
            actualStamina += Mathf.Clamp(regeneration * Time.deltaTime, 0f, maxStamina);
            bar.fillAmount = Mathf.InverseLerp(0, maxStamina, actualStamina);
        }
    }
    public void SpendStamina(float amount)
    {
        actualStamina -= amount;
        recoverCooldown = Time.time + recoverRate;
        bar.fillAmount = Mathf.InverseLerp(0, maxStamina, actualStamina);
    }
    public bool EnoughStamina(float expected)
    {
        if (expected < actualStamina)
            return true;
        return false;
    }
}
