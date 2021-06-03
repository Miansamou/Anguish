using UnityEngine;

public class YmirTalkMyra : MonoBehaviour
{
    [SerializeField]
    private Animator fadeBlackScreen;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private GameObject dialogue;
    [SerializeField]
    private DialogueLine dialogueBox;
    [SerializeField]
    private PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            fadeBlackScreen.SetTrigger("FadeBlack");

            Invoke(nameof(ActiveCamera), 1f);
        }
    }

    private void Update()
    {
        if (dialogue.activeInHierarchy)
        {
            if (!dialogueBox.GetDialogueEnded())
            {
                if (player.GetInteractTrigger() && dialogueBox.GetEndLine())
                    dialogueBox.Play();
            }
            else
            {
                dialogue.gameObject.SetActive(false);
                cam.SetActive(false);
                Invoke(nameof(EnableControl), 0.5f);
            }
        }
    }

    private void ActiveCamera()
    {
        cam.SetActive(true);
        player.DisableControls();
        player.EnableKey("interact");

        Invoke(nameof(ActiveDialogue), 1.5f);
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
