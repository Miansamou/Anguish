using System.Collections.Generic;
using UnityEngine;

public class ObeliskController : MonoBehaviour
{
    public PlayerController player;
    public PlayerItems inventory;
    public GameObject YmirExplanation;
    public DialogueLine YmirDialogue;
    public GameObject TradeMemories;
    public TradeMemories OfferedMemoriesBox;
    public TradeMemories PlayerMemoriesBox;
    public List<ItemObject> AllMemories;

    private List<ItemObject> memoriesGiven;
    private List<ItemObject> memoriesPlayer;
    private Animator anim;

    private void Awake()
    {
        memoriesGiven = new List<ItemObject>();
        memoriesPlayer = new List<ItemObject>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        player.DisableControls();
        player.EnableKey("interact");

        InventoryObject auxInventory = inventory.getMemories();
        foreach(ItemObject obj in auxInventory.inventory)
        {
            memoriesPlayer.Add(obj);
        }

        if (YmirExplanation.activeInHierarchy)
        {
            YmirDialogue.Play();
        }

        OfferedMemoriesBox.UpdateMemories(memoriesGiven);
        PlayerMemoriesBox.UpdateMemories(memoriesPlayer);
    }

    private void Update()
    {
        if (YmirExplanation.activeInHierarchy)
        {
            if (player.GetInteractTrigger())
            {
                if (!YmirDialogue.getDialogueEnded())
                {
                    if (YmirDialogue.getEndLine())
                        YmirDialogue.Play();
                }
                else
                {
                    YmirExplanation.SetActive(false);
                    TradeMemories.SetActive(true);
                }
            }
        }
    }

    public void AddToPlayerMemory(ItemObject memory)
    {
        memoriesPlayer.Add(memory);
        memoriesGiven.Remove(memory);
        OfferedMemoriesBox.UpdateMemories(memoriesGiven);
        PlayerMemoriesBox.UpdateMemories(memoriesPlayer);
    }

    public void AddToOfferedMemory(ItemObject memory)
    {
        memoriesGiven.Add(memory);
        memoriesPlayer.Remove(memory);
        OfferedMemoriesBox.UpdateMemories(memoriesGiven);
        PlayerMemoriesBox.UpdateMemories(memoriesPlayer);
    }

    public void CloseScreen()
    {
        memoriesGiven.Clear();
        memoriesPlayer.Clear();
        player.EnableControls();
        gameObject.SetActive(false);
    }

    public void SaveMemories()
    {
        switch (memoriesGiven.Count)
        {
            case 0:
                anim.SetTrigger("FadeScreen");
                GameScenes.instance.setNextScene(3);
                GameScenes.instance.LoadLevel(1);
                break;
            case 1:
                if (memoriesGiven.Contains(AllMemories[0]))
                {
                    anim.SetTrigger("FadeScreen");
                    GameScenes.instance.setNextScene(4);
                    GameScenes.instance.LoadLevel(1);
                    Debug.Log("Final flor");
                }
                break;
        }
    }
}
