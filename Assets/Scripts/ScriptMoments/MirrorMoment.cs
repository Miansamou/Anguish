using UnityEngine;

public class MirrorMoment : MonoBehaviour
{
    public DialogueLine dialogue;
    public PlayerController player;

    private void OnEnable()
    {
        player.DisableControls();
        player.EnableKey("interact");
        dialogue.ResetDialogue();
        dialogue.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetInteractTrigger())
        {
            player.EnableControls();
            dialogue.Play();

            if (dialogue.GetDialogueEnded() && dialogue.GetEndLine())
            {
                gameObject.SetActive(false);
            }
        }
    }
}
