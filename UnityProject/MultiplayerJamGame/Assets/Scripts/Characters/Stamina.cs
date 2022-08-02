using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float maxStamina = 100;
    private float actualStamina;
    public float regeneration = 10f;

    public Image bar;

    private void Awake()
    {
        actualStamina = maxStamina;
    }
    private void Update()
    {
        if(actualStamina < maxStamina)
        {
            actualStamina += Mathf.Clamp(regeneration * Time.deltaTime, 0f, maxStamina);
            bar.fillAmount = Mathf.InverseLerp(0, maxStamina, actualStamina);
        }
    }
    public void SpendStamina(float amount)
    {
        actualStamina -= amount;
        bar.fillAmount = Mathf.InverseLerp(0, maxStamina, actualStamina);
    }
    public bool EnoughStamina(float expected)
    {
        if (expected < actualStamina)
            return true;
        return false;
    }
}
