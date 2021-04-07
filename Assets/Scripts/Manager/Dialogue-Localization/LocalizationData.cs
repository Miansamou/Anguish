// This script enable to make an dictionary with items array and variables to identify the dialogue for each moment

[System.Serializable]
public class LocalizationData
{
    public LocalizationItems[] items;
}

[System.Serializable]
public class LocalizationItems
{
    public string key;
    public string value;
}
