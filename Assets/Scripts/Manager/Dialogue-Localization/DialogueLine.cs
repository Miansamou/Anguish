using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueLine : DialogueBaseClass
{
    [Header("Text Options")]
    public List<string> key;
    private List<string> backupKey = new List<string>();
    public Color TextColor;
    private int index;
    private bool dialogueEnded;

    [Header("Time")]
    public float Delay;

    [Header("Sound/Voice")]
    public string PlayableSound;

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
            StartCoroutine(WriteText(input, textHolder, TextColor, Delay, PlayableSound));
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

    public bool getDialogueEnded()
    {
        return dialogueEnded;
    }

    public void resetDialogue()
    {
        index = 0;
        dialogueEnded = false;
    }
}
