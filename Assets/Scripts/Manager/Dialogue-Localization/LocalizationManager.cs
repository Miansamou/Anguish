using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    #region variables

    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;
    private readonly string missingTextString = "Text not found";
    private string filePath;
    private string dataAsJson;

    #endregion

    #region initialization

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(instance);

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

    #endregion

    #region get/set

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;

        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;
    }

    #endregion

    #region methods

    // Change the json file used in the Player Preferences and update the texts seing at the scene
    public void ChangeLanguage()
    {
        LocalizedText[] texts = FindObjectsOfType<LocalizedText>();

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

    #endregion
}
