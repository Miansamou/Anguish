using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMoment : MonoBehaviour
{

    public DialogueLine dialogue;
    public PlayerController player;

    private void OnEnable()
    {
        dialogue.resetDialogue();
        dialogue.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getInteractTrigger())
            dialogue.Play();

        if (dialogue.getDialogueEnded())
        {
            player.EnableControls();
            gameObject.SetActive(false);
        }
    }
}
