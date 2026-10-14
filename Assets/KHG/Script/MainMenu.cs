using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject settingsPanel;
    
    [Header("AudioSetting")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private string scenenName;

    private void Awake()
    {
        Buttons();
        OpenSettings();
        CloseSettings();
    }

    private void Buttons()
    {
        if(scenenName != "")return;
        
        if (scenenName == "Exit")
        {
            Application.Quit();
        }
        
        SceneManager.LoadScene(scenenName);
    }
    private void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    private void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
