using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDebug : MonoBehaviour
{
    public AudioManager manager;
    public void PlayMusic()
    {
        manager.Play("music");
    }
    public void PlaySound()
    {
        manager.Play("sound");
    }
}
