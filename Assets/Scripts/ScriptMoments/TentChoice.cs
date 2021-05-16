using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TentChoice : MonoBehaviour
{
    public PlayerController player;
    public PlayerItems inventory;
    public DialogueLine dialogue;

    public Button[] btns;

    public ItemObject[] items;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
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
                        btns[j].onClick.AddListener(GiveCarnation);
                        break;
                    default:
                        Debug.Log("invalid flower");
                        break;
                }
                
                btns[j].gameObject.SetActive(true);
                StartCoroutine(UpdateButtonText(btns[j], LocalizedText.getTextDeterminatedKey(items[i].nameItem)));
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

    public void GiveCarnation()
    {
        Debug.Log("GiveCarnation");
    }

    public void GiveGardenia()
    {
        Debug.Log("GiveGardenia");
    }

    public void GiveWinterberry()
    {
        Debug.Log("GiveWinterberry");
    }

    public void CloseDialogue()
    {
        foreach(Button btn in btns)
        {
            btn.gameObject.SetActive(false);
        }
        anim.SetBool("OptionsActive", false);
        player.EnableControls();
        gameObject.SetActive(false);
    }
}
