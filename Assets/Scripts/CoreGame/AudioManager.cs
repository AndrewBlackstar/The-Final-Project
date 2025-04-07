using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public Sounds[] musicSound, SfxSound;
    public AudioSource musicSource, sfxSource;
    public AudioMixer mixer;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("musicVolume", 1f);
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        mixer.SetFloat("sfx", Mathf.Log10(volume) * 20);

        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "cinematic 1":
                PlayMusic("MenuMusic");
                break;
            case "cinematic 2":
                PlayMusic("Level1Music");
                break;
            case "cinematic 3":
                PlayMusic("BossMusic");
                break;
            case "GameWin":
                PlayMusic("victory");
                break;
            case "GameOver":
                PlayMusic("defeat");
                break;
            default:
                PlayMusic("BGMusic");
                break;
        }

        PlayMusic("BGMusic");
    }

    public void PlayMusic(String name)
    {
        Sounds s = Array.Find(musicSound, x => x.name == name);
        if (s == null)
        {
            Debug.Log("musica no encontrado");
        }

        if (musicSource.clip == s.clip && musicSource.isPlaying)
            return;

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






