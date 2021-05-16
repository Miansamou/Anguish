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
            if (player.getInteractTrigger())
                dialogueBox.Play();

            if (dialogueBox.getDialogueEnded())
            {
                dialogue.gameObject.SetActive(false);
                cam.SetActive(false);
                Invoke("EnableControl", 2f);
            }
        }
    }

    private void ActiveCamera()
    {
        cam.SetActive(true);

        player.DisableKey("jump");
        player.DisableKey("movement");
        player.DisableKey("run");

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
