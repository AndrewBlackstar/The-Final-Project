using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UiMenuControl : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Toggle muteToggle;

    void Start()
    {
     

        if (PlayerPrefs.HasKey("Muted"))
        {
            muteToggle.isOn = PlayerPrefs.GetInt("Muted") == 1;
            SetMute();
        }

        if (PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSfxVolume();
        }


    }

    void Update()
    {
        
    }

    public void LoadScene(string name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }

    public void SetMusicVolume()
    {
        float musicVolume = musicSlider.value;
        audioMixer.SetFloat("Music", MathF.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("Music", musicVolume);

    }

    public void SetSfxVolume()
    {
        float sfxVolume = sfxSlider.value;
        audioMixer.SetFloat("sfx", MathF.Log10(sfxVolume) * 20);
    }

    public void SetMute()
    {
        if (muteToggle.isOn)
        {
            audioMixer.SetFloat("Music", -80f);
            audioMixer.SetFloat("sfx", -80f);
            PlayerPrefs.SetInt("Muted", 1); // Guardamos el estado de mute
        }
        else
        {
            SetMusicVolume();
            SetSfxVolume();
            PlayerPrefs.SetInt("Muted", 0);
        }
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMusicVolume();
        SetSfxVolume();
    }
}
