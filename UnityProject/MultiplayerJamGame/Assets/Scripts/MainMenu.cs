using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer MasterMixer;



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
}
