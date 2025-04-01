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
        Debug.Log("Cargando música: " + PlayerPrefs.GetFloat("musicVolume", 1f));
        Debug.Log("Cargando SFX: " + PlayerPrefs.GetFloat("sfxVolume", 1f));

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);
        // Cargar estado de muteo
        if (PlayerPrefs.HasKey("Muted"))
        {
            muteToggle.isOn = PlayerPrefs.GetInt("Muted") == 1;
            SetMute();
        }

        

        SetMusicVolume();
        SetSfxVolume();
    }

    public void LoadScene(string name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }

    public void SetMusicVolume()
    {
        float musicVolume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();
        Debug.Log("Música guardada: " + PlayerPrefs.GetFloat("musicVolume")); // Verifica que se guarda
    }

    public void SetSfxVolume()
    {
        float sfxVolume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
        Debug.Log("SFX guardado: " + PlayerPrefs.GetFloat("sfxVolume")); // Verifica que se guarda
    }

    public void SetMute()
    {
        if (muteToggle.isOn)
        {
            audioMixer.SetFloat("Music", -80f);
            audioMixer.SetFloat("sfx", -80f);
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            SetMusicVolume();
            SetSfxVolume();
            PlayerPrefs.SetInt("Muted", 0);
        }
        PlayerPrefs.Save();
    }
}
