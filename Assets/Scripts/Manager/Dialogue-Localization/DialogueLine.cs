using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueLine : DialogueBaseClass
{
    [Header("Text Options")]
    public string[] key;
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
        resetDialogue();
    }

    public void Play()
    {
        textHolder = GetComponent<TextMeshProUGUI>();

        if (index < key.Length)
        {
            input = LocalizationManager.instance.GetLocalizedValue(key[index]);
            StartCoroutine(WriteText(input, textHolder, TextColor, Delay, PlayableSound));
        }
        else
            dialogueEnded = true;

        index++;
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
