using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public AudioClip mainMenuSong;
    public AudioClip gameSong;

    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();

        source.clip = mainMenuSong;
    }

    public void SetGameMusic()
    {
        source.Stop();
        source.clip = gameSong;
        source.Play();
    }

    public void SetMainMenuSong()
    {
        source.Stop();
        source.clip = mainMenuSong;
        source.Play();
    }
}
