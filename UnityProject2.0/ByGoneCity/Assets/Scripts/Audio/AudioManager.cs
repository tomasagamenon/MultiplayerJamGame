using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Audio[] sounds;
    public bool dontDestroyOnLoad = false;
    public static AudioManager instance;

    private void Awake()
    {
        if (dontDestroyOnLoad)
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
        foreach (Audio s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatialBlend;
            s.source.playOnAwake = s.playOnAwake;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }
    }
    public void Play(string audioName)
    {
        Audio s = Array.Find(sounds, sound => sound.audioName == audioName);
        if (s == null)
        {
            Debug.LogWarning("Audio file called \"" + audioName + "\" not found");
            return;
        }
        s.source.Play();
    }
    public void PlayOnce(string audioName)
    {
        Audio s = Array.Find(sounds, sound => sound.audioName == audioName);
        if (s == null)
        {
            Debug.LogWarning("Audio file called \"" + audioName + "\" not found");
            return;
        }
        s.source.PlayOneShot(s.clip);
    }
    public void StopPlaying(string audioName)
    {
        Audio s = Array.Find(sounds, sound => sound.audioName == audioName);
        if (s == null)
        {
            Debug.LogWarning("Audio file called \"" + audioName + "\" not found");
            return;
        }
        s.source.Stop();
    }
}
