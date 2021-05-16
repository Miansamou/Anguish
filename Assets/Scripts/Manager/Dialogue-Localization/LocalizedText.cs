using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    TextMeshProUGUI text;
    public string key;
    private string currentLanguage;

    private string newKey;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        newKey = key;
    }

    private void Start()
    {
        UpdateText();
    }

    public void setNewKey(string keyReceived)
    {
        newKey = keyReceived;
    }
    
    // When language is changed and this object is activated, this will update the text to the new language
    private void OnEnable()
    {
        if(currentLanguage != PlayerPrefs.GetString("Language") && !string.IsNullOrEmpty(currentLanguage) ||
            key != newKey)
        {
            UpdateText();
        }
    }
    
    public void UpdateText()
    {
        if(key != newKey)
        {
            key = newKey;
        }
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
        currentLanguage = PlayerPrefs.GetString("Language");
    }

    public void AddText(string textAdded)
    {
        text.text += " " + textAdded;
    }

    public string getTextKey()
    {
        return LocalizationManager.instance.GetLocalizedValue(key);
    }

    public static string getTextDeterminatedKey(string someKey)
    {
        return LocalizationManager.instance.GetLocalizedValue(someKey);
    }
}
