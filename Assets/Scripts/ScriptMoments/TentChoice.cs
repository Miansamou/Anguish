using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TentChoice : MonoBehaviour
{
    #region variables

    [Header("Player")]
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private PlayerItems inventory;
    [SerializeField]
    private PlayerGoals goals;

    [Header("Dialogue")]
    [SerializeField]
    private DialogueLine dialogue;
    [SerializeField]
    private GameObject myra;
    [SerializeField]
    private GameObject darkMyra;

    [Header("Items")]
    [SerializeField]
    private string popUpMessage;
    [SerializeField]
    private ItemObject flowerMemory;
    [SerializeField]
    private GameObject backButton;
    [SerializeField]
    private GameObject tent;
    [SerializeField]
    private Button[] btns;
    [SerializeField]
    private ItemObject[] items;

    private Animator anim;
    private bool inWrongDialogue = false;
    private bool correctDialoguePhase = false;

    #endregion

    #region initialization

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        player.DisableControls();
        player.EnableKey("interact");

        goals.SetMyraGoal("bring_flower");

        dialogue.ResetDialogue();
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

    IEnumerator UpdateButtonText(Button btn, string text)
    {
        LocalizedText localized = btn.GetComponentInChildren<LocalizedText>();
        localized.UpdateText();

        yield return new WaitForSeconds(0.1f);

        localized.AddText(text);
    }

    #endregion

    #region controller

    void Update()
    {
        DialogueController();
    }

    private void DialogueController()
    {
        if ((inWrongDialogue || correctDialoguePhase) && player.GetInteractTrigger())
        {
            if (!dialogue.GetDialogueEnded())
            {
                if (dialogue.GetEndLine())
                    dialogue.Play();
            }
            else if (inWrongDialogue)
            {
                tent.tag = "Untagged";
                goals.SetMyraGoal("end");
                CloseDialogue();
            }
            else if (correctDialoguePhase)
            {
                myra.SetActive(false);
                inventory.AddMemory(flowerMemory, popUpMessage);
                inventory.RemoveItem(items[1]);
                tent.tag = "Untagged";
                goals.SetMyraGoal("end");
                CloseDialogue();
            }
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

    #endregion

    #region give flower

    public void GiveCarnation()
    {
        if (dialogue.GetEndLine())
        {
            dialogue.AddKey("other_flower");
            dialogue.Play();
        }
    }

    public void GiveGardenia()
    {
        if (dialogue.GetEndLine())
        {
            correctDialoguePhase = true;

            foreach (Button btn in btns)
            {
                btn.gameObject.SetActive(false);
            }
            backButton.SetActive(false);
            myra.SetActive(true);
            dialogue.AddKey("correct_flower_1");
            dialogue.AddKey("correct_flower_2");
            dialogue.AddKey("correct_flower_3");
            dialogue.AddKey("correct_flower_4");
            dialogue.AddKey("correct_flower_5");
            dialogue.AddKey("correct_flower_6");
            dialogue.AddKey("correct_flower_7");
            dialogue.AddKey("correct_flower_8");
            dialogue.AddKey("correct_flower_9");
            dialogue.AddKey("correct_flower_10");
            dialogue.AddKey("correct_flower_11");
            dialogue.AddKey("correct_flower_12");
            dialogue.AddKey("correct_flower_13");
            dialogue.AddKey("correct_flower_14");
            dialogue.AddKey("correct_flower_15");
            dialogue.AddKey("correct_flower_16");
            dialogue.AddKey("correct_flower_17");
            dialogue.AddKey("correct_flower_18");
            dialogue.AddKey("correct_flower_19");
            dialogue.AddKey("correct_flower_20");
            dialogue.AddKey("correct_flower_21");
            dialogue.AddKey("correct_flower_22");
            dialogue.Play();
        }
    }

    public void GiveWinterberry()
    {
        if (dialogue.GetEndLine())
        {
            inWrongDialogue = true;

            foreach (Button btn in btns)
            {
                btn.gameObject.SetActive(false);
            }
            backButton.SetActive(false);
            darkMyra.SetActive(true);
            dialogue.AddKey("wrong_flower_1");
            dialogue.AddKey("wrong_flower_2");
            dialogue.AddKey("wrong_flower_3");
            dialogue.AddKey("wrong_flower_4");
            dialogue.Play();
        }
    }

    #endregion
}
