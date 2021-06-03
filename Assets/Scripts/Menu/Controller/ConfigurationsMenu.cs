using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class ConfigurationsMenu : MonoBehaviour
{
    #region variables

    [SerializeField]
    private GameObject menuButtons;
    [SerializeField]
    private Slider masterSound;
    [SerializeField]
    private Slider uiSound;
    [SerializeField]
    private Slider sfxSound;
    [SerializeField]
    private Slider musicSound;
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private TMP_Dropdown language;
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    [SerializeField]
    private Toggle fullScreen;
    [SerializeField]
    private GameObject[] settings;

    private Resolution[] resolutions;
    private bool updateDisable;
    private bool updateEnable;
    private int page;

    #endregion

    #region initialization

    void Start()
    {
        updateEnable = false;
        updateDisable = true;
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

    #endregion

    #region update settings

    void Update()
    {
        StartUpdate();
    }

    private void StartUpdate()
    {
        if (menuButtons.activeInHierarchy)
        {
            if (!updateEnable)
            {
                masterSound.value = PlayerPrefs.GetFloat("MasterVolume");
                uiSound.value = PlayerPrefs.GetFloat("UIVolume");
                sfxSound.value = PlayerPrefs.GetFloat("SFXVolume");
                musicSound.value = PlayerPrefs.GetFloat("MusicVolume");

                UpdateLocalizationDropdown();
                UpdateToggle();

                page = 0;
                foreach (GameObject setting in settings)
                {
                    setting.SetActive(false);
                }

                settings[page].SetActive(true);

                updateEnable = true;
                updateDisable = false;
            }
        }
        else
        {
            if (!updateDisable)
            {
                UpdateSound();

                updateEnable = false;
                updateDisable = true;
            }
        }
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
        if (language.value == 0)
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
            language.value = 0;
        }
        else
            language.value = 1;
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

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SaveSettings()
    {
        AudioManager.instance.Play("ClickButton");

        PlayerPrefs.SetFloat("MasterVolume", masterSound.value);
        PlayerPrefs.SetFloat("UIVolume", uiSound.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSound.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSound.value);

        UpdateLocalizationPref();
        LocalizationManager.instance.ChangeLanguage();

        SetResolution(resolutionDropdown.value);

        UpdateFullScreen();
        UpdateToggle();
    }

    #endregion

    #region Buttons Options

    public void NextSetting()
    {
        AudioManager.instance.Play("ClickButton");
        settings[page].SetActive(false);
        page++;
        if (page >= settings.Length)
            page = 0;
        settings[page].SetActive(true);
    }

    public void PreviousSetting()
    {
        AudioManager.instance.Play("ClickButton");
        settings[page].SetActive(false);
        page--;
        if (page < 0)
            page = settings.Length - 1;
        settings[page].SetActive(true);
    }

    public void PlusAudio(string mixerGroup)
    {
        AudioManager.instance.Play("ClickButton");
        int value = 0;
        switch (mixerGroup)
        {
            case "MasterVolume":
                masterSound.value++;
                value = (int)masterSound.value;
                break;
            case "UIVolume":
                uiSound.value++;
                value = (int)uiSound.value;
                break;
            case "SFXVolume":
                sfxSound.value++;
                value = (int)sfxSound.value;
                break;
            case "MusicVolume":
                musicSound.value++;
                value = (int)musicSound.value;
                break;
        }

        UpdateSlider(mixerGroup, value);
    }

    public void MinusAudio(string mixerGroup)
    {
        AudioManager.instance.Play("ClickButton");
        int value = 0;
        switch (mixerGroup)
        {
            case "MasterVolume":
                masterSound.value--;
                value = (int)masterSound.value;
                break;
            case "UIVolume":
                uiSound.value--;
                value = (int)uiSound.value;
                break;
            case "SFXVolume":
                sfxSound.value--;
                value = (int)sfxSound.value;
                break;
            case "MusicVolume":
                musicSound.value--;
                value = (int)musicSound.value;
                break;
        }

        UpdateSlider(mixerGroup, value);
    }

    #endregion
}
