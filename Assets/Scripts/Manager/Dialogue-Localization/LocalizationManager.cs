using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// This is how we create a list of the current language keys, and modify the texts
// on the game.

public class LocalizationManager : MonoBehaviour
{

    // Instance to use this same datas in other scripts
    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;
    private string missingTextString = "Text not found";


    private string filePath;
    private string dataAsJson;

    void Awake()
    {
        // Don't duplicate the LocalizationManager object at the scene
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Set first language in the Player Preferences based on  the System Language 
        // of the user's devices
        string languageFile;

        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Portuguese)
            {
                languageFile = "localizedText_BR.json";
            }
            else
            {
                languageFile = "localizedText_EN.json";
            }
            
            PlayerPrefs.SetString("Language", languageFile);
        }

        LoadLocalizedText(PlayerPrefs.GetString("Language"));
    }

    // Change the json file used in the Player Preferences and update the texts seing at the scene
    public void ChangeLanguage()
    {
        LocalizedText[] texts = FindObjectsOfType<LocalizedText>();

        if (PlayerPrefs.GetString("Language") == "localizedText_BR.json")
        {
            PlayerPrefs.SetString("Language", "localizedText_EN.json");
        }
        else
            PlayerPrefs.SetString("Language", "localizedText_BR.json");

        LoadLocalizedText(PlayerPrefs.GetString("Language"));

        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].UpdateText();
        }
    }

    // Make the list whith the key and value 
    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();

        filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        dataAsJson = File.ReadAllText(filePath);

        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

    }

    // Receive the key and return the dialogue
    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;

        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;
    }
}
