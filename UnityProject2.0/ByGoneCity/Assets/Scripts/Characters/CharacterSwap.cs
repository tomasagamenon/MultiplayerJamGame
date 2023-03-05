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
        orcEnabled = orc.GetComponent<PlayerInput>().isActiveAndEnabled;
        gnomeEnabled = gnome.GetComponent<PlayerInput>().isActiveAndEnabled;
    }
    public void OnSwap(InputAction.CallbackContext input)
    {
        if (input.performed)
            Swap();
    }
    private void Swap()
    {
        orcEnabled = !orcEnabled;
        orc.GetComponent<PlayerInput>().enabled = orcEnabled;
        gnomeEnabled = !gnomeEnabled;
        gnome.GetComponent<PlayerInput>().enabled = gnomeEnabled;
    }
}
