using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (var sound in sounds)
        {
            sound.AssignSource(gameObject.AddComponent<AudioSource>());
        }
    }

    public void Play(string soundName)
    {
        var match = Array.Find(sounds, sound => sound.name == soundName);
        if (match == null)
        {
            Debug.LogWarning($"Sound {soundName} not found");
            return;
        }
        match.source.Play();
    }
}
