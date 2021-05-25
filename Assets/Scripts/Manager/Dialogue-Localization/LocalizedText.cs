using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    private string key;
    public bool updateTextByKey;

    private TextMeshProUGUI text;
    private string currentLanguage;
    private string newKey;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateText();

        if (string.IsNullOrEmpty(newKey))
            newKey = key;
    }

    public void SetNewKey(string keyReceived)
    {
        newKey = keyReceived;
    }
    
    // When language is changed and this object is activated, this will update the text to the new language
    private void OnEnable()
    {
        if(currentLanguage != PlayerPrefs.GetString("Language") && !string.IsNullOrEmpty(currentLanguage))
        {
            UpdateText();
        }
    }

    private void Update()
    {
        if (key != newKey && updateTextByKey)
        {
            key = newKey;
            UpdateText();
        }
    }

    public void UpdateText()
    {
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
        currentLanguage = PlayerPrefs.GetString("Language");
    }

    public void AddText(string textAdded)
    {
        text.text += " " + textAdded;
    }

    public string GetTextKey()
    {
        return LocalizationManager.instance.GetLocalizedValue(key);
    }

    public static string GetTextDeterminatedKey(string someKey)
    {
        return LocalizationManager.instance.GetLocalizedValue(someKey);
    }
}
