using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TentChoice : MonoBehaviour
{
    public PlayerController player;
    public PlayerItems inventory;
    public DialogueLine dialogue;
    public ItemObject FlowerMemory;
    public string[] RemedyKeys;

    public Button[] btns;

    public ItemObject[] items;

    public GameObject Tent;
    public GameObject Myra;
    public GameObject DarkMyra;
    public GameObject backButton;

    private Animator anim;
    private bool inWrongDialogue = false;
    private int correctDialoguePhase = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        player.DisableControls();
        player.EnableKey("interact");

        if(correctDialoguePhase > 0)
        {
            Myra.SetActive(true);
            dialogue.ClearText();
            foreach(string key in RemedyKeys)
            {
                dialogue.AddKey(key);
            }
            dialogue.Play();
            return;
        }

        dialogue.resetDialogue();
        dialogue.Play();

        int j = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (inventory.FindItemInInventory(items[i].nameItem))
            {
                switch (items[i].nameItem)
                {
                    case "flower_carnation":
                        btns[j].onClick.AddListener(GiveCarnation);
                        break;
                    case "flower_gardenia":
                        btns[j].onClick.AddListener(GiveGardenia);
                        break;
                    case "flower_winterberry":
                        btns[j].onClick.AddListener(GiveWinterberry);
                        break;
                    default:
                        Debug.Log("invalid flower");
                        break;
                }
                
                btns[j].gameObject.SetActive(true);
                StartCoroutine(UpdateButtonText(btns[j], LocalizedText.GetTextDeterminatedKey(items[i].nameItem)));
                j++;
            }
        }
        anim.SetBool("OptionsActive", true);
    }

    void Update()
    {
        if ((inWrongDialogue || correctDialoguePhase > 0) && player.GetInteractTrigger())
        {
            if (!dialogue.getDialogueEnded())
            {
                if (dialogue.getEndLine())
                    dialogue.Play();
            }
            else if(inWrongDialogue)
            {
                Tent.tag = "Untagged";
                CloseDialogue();
            }
            else if (correctDialoguePhase == 1)
            {
                correctDialoguePhase = 2;
                Myra.SetActive(false);
                dialogue.AddKey("receive_flower_memory");
                inventory.AddMemory(FlowerMemory);
                inventory.RemoveItem(items[1]);
                dialogue.Play();
            }
            else if (correctDialoguePhase == 2)
            {
                correctDialoguePhase = 3;
                CloseDialogue();
            }
            else if(correctDialoguePhase == 3)
            {
                CloseDialogue();
            }
        }
    }

    IEnumerator UpdateButtonText(Button btn, string text)
    {
        LocalizedText localized = btn.GetComponentInChildren<LocalizedText>();
        localized.UpdateText();
        
        yield return new WaitForSeconds(0.1f);

        localized.AddText(text);
    }

    public void GiveCarnation()
    {
        if (dialogue.getEndLine())
        {
            dialogue.AddKey("other_flower");
            dialogue.Play();
        }
    }

    public void GiveGardenia()
    {
        if (dialogue.getEndLine())
        {
            correctDialoguePhase = 1;

            foreach (Button btn in btns)
            {
                btn.gameObject.SetActive(false);
            }
            backButton.SetActive(false);
            Myra.SetActive(true);
            dialogue.AddKey("correct_flower_1");
            dialogue.AddKey("correct_flower_2");
            dialogue.AddKey("correct_flower_3");
            dialogue.AddKey("correct_flower_4");
            dialogue.AddKey("correct_flower_5");
            dialogue.AddKey("correct_flower_6");
            dialogue.Play();
        }
    }

    public void GiveWinterberry()
    {
        if (dialogue.getEndLine())
        {
            inWrongDialogue = true;

            foreach (Button btn in btns)
            {
                btn.gameObject.SetActive(false);
            }
            backButton.SetActive(false);
            DarkMyra.SetActive(true);
            dialogue.AddKey("wrong_flower_1");
            dialogue.AddKey("wrong_flower_2");
            dialogue.AddKey("wrong_flower_3");
            dialogue.Play();
        }
    }

    public void CloseDialogue()
    {
        dialogue.ResetDefault();
        foreach (Button btn in btns)
        {
            btn.gameObject.SetActive(false);
        }
        anim.SetBool("OptionsActive", false);
        player.EnableControls();
        gameObject.SetActive(false);
    }
}
