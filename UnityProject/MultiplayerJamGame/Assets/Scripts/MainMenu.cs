using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer MasterMixer;
    private bool level;
    public GameObject gameMenu;
    private void Awake()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneAt(0) || SceneManager.GetActiveScene() == SceneManager.GetSceneAt(1)
            || SceneManager.GetActiveScene() == SceneManager.GetSceneAt(2))
        {
            level = false;
        }
        else
        {
            level = true;
        }
    }
    private void Update()
    {
        if (level && Input.GetButtonDown("Cancel"))
        {
            ToggleMenu();
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        if(Application.platform != RuntimePlatform.WebGLPlayer)
            Application.Quit();
    }
    public void MasterVolume(float volume)
    {
        MasterMixer.SetFloat("MasterVolume", volume);
    }
    public void SFXVolume(float volume)
    {
        MasterMixer.SetFloat("SFXVolume", volume);
    }
    public void MusicVolume(float volume)
    {
        MasterMixer.SetFloat("MusicVolume", volume);
    }
    public void ToggleMenu()
    {
        gameMenu.SetActive(!gameMenu.activeSelf);
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
