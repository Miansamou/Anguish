using UnityEngine;

public class MirrorMoment : MonoBehaviour
{
    public DialogueLine dialogue;
    public PlayerController player;

    private void OnEnable()
    {
        player.DisableControls();
        player.EnableKey("interact");
        dialogue.resetDialogue();
        dialogue.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getInteractTrigger())
        {
            player.EnableControls();
            dialogue.Play();
        }

        if (dialogue.getDialogueEnded())
        {
            gameObject.SetActive(false);
        }
    }
}
