using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public Sounds[] musicSound, SfxSound;
    public AudioSource musicSource, sfxSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }

    }

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 1f); // Si no hay valor guardado, usa 1
        musicSource.volume = volume;
        PlayMusic("BGMusic");
    }

    public void PlayMusic(String name)
    {
        Sounds s = Array.Find(musicSound, x => x.name == name);
        if (s == null)
        {
            Debug.Log("musica no encontrado");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.mute = false;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sounds s = Array.Find(SfxSound, x => x.name == name);
        if (s == null)
        {
            Debug.Log("sfx no encontrado");
        }

        else
        {
            sfxSource.clip = s.clip;
            musicSource.mute = false;
            sfxSource.Play();
        }
    }


}






