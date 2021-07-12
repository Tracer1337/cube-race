using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0, 1)]
    public float volume;
    
    [HideInInspector]
    public AudioSource source;

    public void AssignSource(AudioSource newSource)
    {
        source = newSource;
        source.clip = clip;
        source.volume = volume;
    }
}