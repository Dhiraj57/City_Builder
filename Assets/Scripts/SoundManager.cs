using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance { get { return instance; } }

    public SoundType[] sounds;

    public AudioSource soundEffect;
    public AudioSource soundMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(SoundManager.Sounds.Music);
    }

    public void PlayMusic(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Play();
        }
    }

    public void Play(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
    }

    private AudioClip getSoundClip(Sounds sound)
    {
        SoundType returnSound = Array.Find(sounds, item => item.soundType == sound);
        return returnSound.soundClip;
    }

    [Serializable]
    public class SoundType
    {
        public Sounds soundType;
        public AudioClip soundClip;
    }

    public enum Sounds
    {
        ButtonClick,
        Music,
        Spawn,
        Remove
    }
}
