using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public AudioClip mainMenuSong;
    public AudioClip gameSong;
    public AudioClip winSound;
    public AudioClip loseSound;

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

    public void PlayWinLoseSound(bool isGameOver)
    {
        source.Stop();
        source.loop = false;

        if (isGameOver)
            source.clip = loseSound;
        else
            source.clip = winSound;

        source.Play();
    }

    public void SetMenuGameMusic(bool isMainMenu)
    {
        source.Stop();
        source.loop = true;

        if (isMainMenu)
            source.clip = mainMenuSong;
        else
            source.clip = gameSong;

        source.Play();
    }
}
