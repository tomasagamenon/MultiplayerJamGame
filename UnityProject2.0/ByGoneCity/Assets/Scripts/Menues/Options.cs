using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixer masterMixer;
    
    public void MasterVolume(float volume)
    {
        masterMixer.SetFloat("MasterVolume", volume);
    }
    public void MusicVolume(float volume)
    {
        masterMixer.SetFloat("MusicVolume", volume);
    }
    public void SFXVolume(float volume)
    {
        masterMixer.SetFloat("SFXVolume", volume);
    }
}
