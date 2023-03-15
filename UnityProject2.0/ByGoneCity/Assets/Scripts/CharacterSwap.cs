using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwap : MonoBehaviour
{
    public GameObject orc;
    public string orcControlScheme;
    private bool orcEnabled;
    public GameObject gnome;
    public string gnomeControlScheme;
    private bool gnomeEnabled;
    private void Awake()
    {
        orcEnabled = orc.GetComponent<PlayerInput>().enabled;
        gnomeEnabled = gnome.GetComponent<PlayerInput>().enabled;
        //PlayerInput.Instantiate()
        GetComponent<PlayerInput>().SwitchCurrentControlScheme("Debug", Keyboard.current);
        gnome.GetComponent<PlayerInput>().SwitchCurrentControlScheme(gnomeControlScheme, Keyboard.current);
        orc.GetComponent<PlayerInput>().SwitchCurrentControlScheme(orcControlScheme, Keyboard.current);
    }
    public void OnSwap(InputAction.CallbackContext input)
    {
        if (input.performed)
            Swap();
    }
    private void Swap()
    {
        // hacer manualmente que el scheme o controller o lo que sea, del personaje sea el teclado
        orcEnabled = !orcEnabled;
        //orc.SetActive(orcEnabled);
        orc.GetComponent<PlayerInput>().enabled = orcEnabled;
        orc.GetComponent<PlayerInput>().SwitchCurrentControlScheme(orcControlScheme, Keyboard.current);
        gnomeEnabled = !gnomeEnabled;
        //gnome.SetActive(gnomeEnabled);
        gnome.GetComponent<PlayerInput>().enabled = gnomeEnabled;
        gnome.GetComponent<PlayerInput>().SwitchCurrentControlScheme(gnomeControlScheme, Keyboard.current);
    }
}
