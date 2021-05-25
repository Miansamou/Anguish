using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class ConfigurationsMenu : MonoBehaviour
{
    public Slider MasterSound;
    public Slider UISound;
    public Slider SFXSound;
    public Slider MusicSound;
    public AudioMixer mixer;

    public TMP_Dropdown Language;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreen;

    public GameObject[] settings;
    private int page;

    public GameObject MenuButtons;
    private bool UpdateEnable;
    private bool UpdateDisable;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        UpdateEnable = false;
        UpdateDisable = true;
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        UpdateSound();
    }

    private void UpdateSound()
    {
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            PlayerPrefs.SetFloat("MasterVolume", 8f);
        }
        if (!PlayerPrefs.HasKey("UIVolume"))
        {
            PlayerPrefs.SetFloat("UIVolume", 8f);
        }
        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 8f);
        }
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 8f);
        }

        UpdateSlider("MusicVolume", (int)PlayerPrefs.GetFloat("MusicVolume"));
        UpdateSlider("SFXVolume", (int)PlayerPrefs.GetFloat("SFXVolume"));
        UpdateSlider("UIVolume", (int)PlayerPrefs.GetFloat("UIVolume"));
        UpdateSlider("MasterVolume", (int)PlayerPrefs.GetFloat("MasterVolume"));
    }

    public void UpdateToggle()
    {
        if (!PlayerPrefs.HasKey("FullScreen"))
        {
            PlayerPrefs.SetString("FullScreen", "true");
            Screen.fullScreen = true;
        }

        if(PlayerPrefs.GetString("FullScreen") == "true")
        {
            Screen.fullScreen = true;
            fullScreen.isOn = true;
        }
        else
        {
            Screen.fullScreen = false;
            fullScreen.isOn = false;
        }
    }

    public void UpdateFullScreen()
    {
        if (fullScreen.isOn)
        {
            PlayerPrefs.SetString("FullScreen", "true");
        }
        else
            PlayerPrefs.SetString("FullScreen", "false");
    }

    public void UpdateLocalizationPref()
    {
        if (Language.value == 0)
        {
            PlayerPrefs.SetString("Language", "localizedText_BR.json");
        }
        else
            PlayerPrefs.SetString("Language", "localizedText_EN.json");
    }

    private void UpdateLocalizationDropdown()
    {
        if(PlayerPrefs.GetString("Language") == "localizedText_BR.json")
        {
            Language.value = 0;
        }
        else
            Language.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuButtons.activeInHierarchy)
        {
            if (!UpdateEnable)
            {
                MasterSound.value = PlayerPrefs.GetFloat("MasterVolume");
                UISound.value = PlayerPrefs.GetFloat("UIVolume");
                SFXSound.value = PlayerPrefs.GetFloat("SFXVolume");
                MusicSound.value = PlayerPrefs.GetFloat("MusicVolume");

                UpdateLocalizationDropdown();
                UpdateToggle();

                page = 0;
                foreach (GameObject setting in settings)
                {
                    setting.SetActive(false);
                }

                settings[page].SetActive(true);

                UpdateEnable = true;
                UpdateDisable = false;
            }
        }
        else
        {
            if (!UpdateDisable)
            {
                UpdateSound();

                UpdateEnable = false;
                UpdateDisable = true;
            }
        }
    }

    public void NextSetting()
    {
        settings[page].SetActive(false);
        page++;
        if (page >= settings.Length)
            page = 0;
        settings[page].SetActive(true);
    }

    public void PreviousSetting()
    {
        settings[page].SetActive(false);
        page--;
        if (page < 0)
            page = settings.Length - 1;
        settings[page].SetActive(true);
    }

    public void PlusAudio(string mixerGroup)
    {
        int value = 0;
        switch (mixerGroup)
        {
            case "MasterVolume":
                MasterSound.value++;
                value = (int)MasterSound.value;
                break;
            case "UIVolume":
                UISound.value++;
                value = (int)UISound.value;
                break;
            case "SFXVolume":
                SFXSound.value++;
                value = (int)SFXSound.value;
                break;
            case "MusicVolume":
                MusicSound.value++;
                value = (int)MusicSound.value;
                break;
        }

        UpdateSlider(mixerGroup, value);
    }

    public void MinusAudio(string mixerGroup)
    {
        int value = 0;
        switch (mixerGroup)
        {
            case "MasterVolume":
                MasterSound.value--;
                value = (int)MasterSound.value;
                break;
            case "UIVolume":
                UISound.value--;
                value = (int)UISound.value;
                break;
            case "SFXVolume":
                SFXSound.value--;
                value = (int)SFXSound.value;
                break;
            case "MusicVolume":
                MusicSound.value--;
                value = (int)MusicSound.value;
                break;
        }

        UpdateSlider(mixerGroup, value);
    }

    public void SetResolution(int resolutionIndex) 
    { 
        Resolution resolution = resolutions[resolutionIndex]; 
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); 
    }

    private void UpdateSlider(string mixerGroup, int volume)
    {
        switch (volume)
        {
            case 0:
                mixer.SetFloat(mixerGroup, -88);
                break;
            case 1:
                mixer.SetFloat(mixerGroup, -40);
                break;
            case 2:
                mixer.SetFloat(mixerGroup, -30);
                break;
            case 3:
                mixer.SetFloat(mixerGroup, -25);
                break;
            case 4:
                mixer.SetFloat(mixerGroup, -20);
                break;
            case 5:
                mixer.SetFloat(mixerGroup, -15);
                break;
            case 6:
                mixer.SetFloat(mixerGroup, -10);
                break;
            case 7:
                mixer.SetFloat(mixerGroup, -5);
                break;
            case 8:
                mixer.SetFloat(mixerGroup, 0);
                break;
            case 9:
                mixer.SetFloat(mixerGroup, 10);
                break;
            default:
                mixer.SetFloat(mixerGroup, 0);
                break;
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterSound.value);
        PlayerPrefs.SetFloat("UIVolume", UISound.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXSound.value);
        PlayerPrefs.SetFloat("MusicVolume", MusicSound.value);

        UpdateLocalizationPref();
        LocalizationManager.instance.ChangeLanguage();

        SetResolution(resolutionDropdown.value);

        UpdateFullScreen();
        UpdateToggle();
    }
}
