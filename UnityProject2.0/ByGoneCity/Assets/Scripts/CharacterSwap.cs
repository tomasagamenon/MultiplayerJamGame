using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwap : MonoBehaviour
{
    public GameObject orc;
    public string orcControlScheme;
    private PlayerInput orcInput;
    private bool orcEnabled;
    public GameObject gnome;
    public string gnomeControlScheme;
    private PlayerInput gnomeInput;
    private bool gnomeEnabled;
    private void Awake()
    {
        orcEnabled = orc.GetComponent<PlayerInput>().enabled;
        gnomeEnabled = gnome.GetComponent<PlayerInput>().enabled;
        //PlayerInput.Instantiate()
        gnomeInput = gnome.GetComponent<PlayerInput>();
        gnomeInput.SwitchCurrentControlScheme(gnomeControlScheme, Keyboard.current);
        orcInput = orc.GetComponent<PlayerInput>();
        orcInput.SwitchCurrentControlScheme(orcControlScheme, Keyboard.current);
        GetComponent<PlayerInput>().SwitchCurrentControlScheme("Debug", Keyboard.current);
    }
    public void OnSwap(InputAction.CallbackContext input)
    {
        if (input.performed)
            Swap();
    }
    private void Swap()
    {
        if (gnomeEnabled && orcEnabled)
        {
            orcEnabled = false;
            orcInput.enabled = false;
        }
        else if (gnomeEnabled)
        {
            orcEnabled = true;
            orcInput.enabled = true;
            gnomeEnabled = false;
            gnomeInput.enabled = false;
        }
        else
        {
            gnomeEnabled = true;
            gnomeInput.enabled = true;
        }
        orcInput.SwitchCurrentControlScheme(orcControlScheme, Keyboard.current);
        gnomeInput.SwitchCurrentControlScheme(gnomeControlScheme, Keyboard.current);
    }
}
