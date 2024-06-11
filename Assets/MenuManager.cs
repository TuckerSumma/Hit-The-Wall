using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Diagnostics;
using System;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI output;
    public Button playButton, exitButton, optionsButton;
    public AudioMixer audioMixer;
    public AudioMixer sfxMixer;
    public Slider volSlider;
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public Toggle fullscreen;
    static bool isFirstLoad = true;
    static int currentResolutionIndex;
    int[] savedResolutions;
    public TextMeshProUGUI resTest;
    float height;
    float width;
    float ratio;

    void Awake()
    {
        resolutions = Screen.resolutions;
        savedResolutions = new int[resolutions.Length];
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            height = resolutions[i].height;
            width = resolutions[i].width;
            ratio = width / height;
            if (ratio == 16.0f/9.0f)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height + " " + Convert.ToInt32(resolutions[i].refreshRateRatio.value) + "Hz";
                options.Add(option);
                savedResolutions[options.Count-1] = i;

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && isFirstLoad)
                {
                    currentResolutionIndex = i;
                }  
            }
        }
        resolutionDropdown.AddOptions(options);

        if (isFirstLoad)
        {
            Resolution resolution = resolutions[savedResolutions[currentResolutionIndex]];
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            fullscreen.isOn = Screen.fullScreen;
            isFirstLoad = false;
        }
        else
        {
            UnityEngine.Debug.Log("Rememberd");
            RememberValues();
        }
        
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[savedResolutions[resolutionIndex]];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Application.targetFrameRate = Convert.ToInt32(resolution.refreshRateRatio.value);
        currentResolutionIndex = resolutionIndex;
    }
    public void Play()
    {
        SceneManager.LoadScene("Test Level");
    }

    public void Exit()
    {
        Application.Quit();
        UnityEngine.Debug.Log("Game Has Closed"); 
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        sfxMixer.SetFloat("MasterVolume", volume);

    }
    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        
    }

    void RememberValues()
    {
        audioMixer.GetFloat("MasterVolume", out float volumeValue);
        volSlider.value = volumeValue;

        UnityEngine.Debug.Log(currentResolutionIndex);
        UnityEngine.Debug.Log(Screen.currentResolution.width);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        fullscreen.isOn = Screen.fullScreen;
    }
}
