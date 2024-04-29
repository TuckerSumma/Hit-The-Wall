using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI output;
    public Button playButton, exitButton, optionsButton;
    public AudioMixer audioMixer;
    public Slider volSlider;
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public Toggle fullscreen;
    static bool isFirstLoad = true;
    static int currentResolutionIndex;

    void Awake()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if ((isFirstLoad) )
            {
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                    
                }
                
            }
            
        }
        resolutionDropdown.AddOptions(options);

        if (isFirstLoad)
        {
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            isFirstLoad = false;
        }
        else
        {
            RememberValues();
        }
        
    }
    public void SetResolution(int resolutionIndex)
    {
        currentResolutionIndex = resolutionIndex;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void Play()
    {
        SceneManager.LoadScene("Test Level");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Game Has Closed"); 
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        
    }

    void RememberValues()
    {
        audioMixer.GetFloat("MasterVolume", out float volumeValue);
        volSlider.value = volumeValue;

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        fullscreen.isOn = Screen.fullScreen;
    }
}
