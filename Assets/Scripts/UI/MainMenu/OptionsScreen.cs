using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour
{
    // Resolution
    Resolution[] resolutions;

    // Button
    public Button backButton;

    // Options
    public AudioMixer audioMixer;
    public Slider slider;
    public Dropdown qualityDropdown;
    public Dropdown resolutionDropdown;
    public Toggle toggle;

    // --------------------------------------------------------------------------------------------------------- //    

	void Start ()
    {
        if (backButton != null) backButton.onClick.AddListener(BackButtonPressed);
        if (qualityDropdown != null) qualityDropdown.onValueChanged.AddListener(SetQuality);
        if (resolutionDropdown != null) resolutionDropdown.onValueChanged.AddListener(SetResolution);
        if (slider != null) slider.onValueChanged.AddListener(SetVolume);
        if (toggle != null) toggle.onValueChanged.AddListener(SetFullScreen);

        GetResolutions();
        GetQualityLevel();
        IsFullscreen();


        
        
    }

    // --------------------------------------------------------------------------------------------------------------------
    // Setting options..

    public void BackButtonPressed()
    {
        MainMenuUIManager.instance.ToMainMenuScreen();
    }

    public void SetVolume(float val)
    {
        audioMixer.SetFloat("Volume", val);
        Debug.Log(val);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // --------------------------------------------------------------------------------------------------------------------
    // Getting options..

    public void GetResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void GetQualityLevel()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void IsFullscreen()
    {
        toggle.isOn = Screen.fullScreen;
    }

}
