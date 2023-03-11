using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwap : MonoBehaviour
{
    public GameObject orc;
    private bool orcEnabled;
    public GameObject gnome;
    private bool gnomeEnabled;
    private void Awake()
    {
        orcEnabled = orc.activeSelf;
        gnomeEnabled = gnome.activeSelf;
    }
    public void OnSwap(InputAction.CallbackContext input)
    {
        if (input.performed)
            Swap();
    }
    private void Swap()
    {
        orcEnabled = !orcEnabled;
        orc.SetActive(orcEnabled);
        //orc.GetComponent<PlayerInput>().enabled = orcEnabled;
        gnomeEnabled = !gnomeEnabled;
        gnome.SetActive(gnomeEnabled);
        //gnome.GetComponent<PlayerInput>().enabled = gnomeEnabled;
    }
}
