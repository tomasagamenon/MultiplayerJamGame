using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTemp : MonoBehaviour
{
    public GameObject[] shieldGirl, swordGuy;

    public void Swap()
    {
        foreach (GameObject shield in shieldGirl)
        {
            shield.SetActive(!shield.activeSelf);
        }
        foreach (GameObject sword in swordGuy)
        {
            sword.SetActive(!sword.activeSelf);
        }
    }
}
