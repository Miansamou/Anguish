using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueLine : DialogueBaseClass
{
    [Header("Text Options")]
    [SerializeField]
    private List<string> key;
    [SerializeField]
    private Color TextColor;
    private List<string> backupKey = new List<string>();
    private int index;
    private bool dialogueEnded;

    [Header("Time")]
    public float Delay;

    [Header("Sound/Voice")]
    [SerializeField]
    private string playableSound;

    private TextMeshProUGUI textHolder;
    private string input;

    private void Awake()
    {
        foreach (string auxKey in key)
        {
            backupKey.Add(auxKey);
        }
    }

    public void Play()
    {
        textHolder = GetComponent<TextMeshProUGUI>();
        if (index < key.Count)
        {
            input = LocalizationManager.instance.GetLocalizedValue(key[index]);
            StartCoroutine(WriteText(input, textHolder, TextColor, Delay, playableSound));
        }
        else
            dialogueEnded = true;

        index++;
    }

    public void ClearText()
    {
        key.Clear();
    }

    public void AddKey(string newText)
    {
        key.Add(newText);
    }

    public void ResetDefault()
    {
        ClearText();
        foreach (string auxKey in backupKey)
        {
            key.Add(auxKey);
        }
    }

    public bool GetDialogueEnded()
    {
        if(index >= key.Count)
        {
            dialogueEnded = true;
        }
        return dialogueEnded;
    }

    public void ResetDialogue()
    {
        index = 0;
        dialogueEnded = false;
    }
}
