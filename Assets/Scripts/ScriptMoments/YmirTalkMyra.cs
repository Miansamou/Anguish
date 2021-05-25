using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YmirTalkMyra : MonoBehaviour
{
    public Animator fadeBlackScreen;
    public GameObject cam;
    public GameObject dialogue;
    public DialogueLine dialogueBox;
    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            fadeBlackScreen.SetTrigger("FadeBlack");

            Invoke("ActiveCamera", 1f);
        }
    }

    private void Update()
    {
        if (dialogue.activeInHierarchy)
        {
            if (!dialogueBox.getDialogueEnded())
            {
                if (player.getInteractTrigger() && dialogueBox.getEndLine())
                    dialogueBox.Play();
            }
            else
            {
                dialogue.gameObject.SetActive(false);
                cam.SetActive(false);
                Invoke("EnableControl", 0.5f);
            }
        }
    }

    private void ActiveCamera()
    {
        cam.SetActive(true);
        player.DisableControls();
        player.EnableKey("interact");

        Invoke("ActiveDialogue", 1.5f);
    }

    private void ActiveDialogue()
    {
        dialogue.gameObject.SetActive(true);
        dialogueBox.Play();
    }

    private void EnableControl()
    {
        player.EnableControls();
        gameObject.SetActive(false);
    }
}
