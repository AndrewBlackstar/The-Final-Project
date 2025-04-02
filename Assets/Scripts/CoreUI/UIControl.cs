using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class UIControl : MonoBehaviour
{
    bool isPaused = false;
    public GameObject pausePanel;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Toggle muteToggle;

    void Start()
    {
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Cargar mute si está guardado
        if (PlayerPrefs.HasKey("Muted"))
        {
            muteToggle.isOn = PlayerPrefs.GetInt("Muted") == 1;
            SetMute();
        }

        // Cargar valores de volumen guardados
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicSlider.value = 1f; // Valor por defecto
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            sfxSlider.value = 1f; // Valor por defecto
        }

        Debug.Log("Volumen inicial - Música: " + musicSlider.value + ", SFX: " + sfxSlider.value);

        SetMusicVolume();
        SetSfxVolume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void LateUpdate()
    {
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
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
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();
        Debug.Log("Música guardada: " + musicVolume);
    }

    public void SetSfxVolume()
    {
        float sfxVolume = sfxSlider.value;
        audioMixer.SetFloat("sfx", MathF.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
        Debug.Log("SFX guardado: " + sfxVolume);
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
