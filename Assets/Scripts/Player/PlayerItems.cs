using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public InventoryObject itemsForUse;
    public InventoryObject documents;
    public InventoryObject memories;

    public GameObject ConfirmationGetItems;
    public GameObject ConfirmationUseItems;
    public GameObject PoPUpMessage;

    private Animator animConfirmationGetItems;
    private Animator animConfirmationUseItems;
    private Animator animPoPUp;
    private LocalizedText localizedConfirmationGetItems;
    private LocalizedText localizedConfirmationUseItems;
    private LocalizedText localizedPopUp;
    private CollectableItem collectableItem;
    private Interactable interactableItem;

    private void Start()
    {
        animConfirmationGetItems = ConfirmationGetItems.GetComponent<Animator>();
        animConfirmationUseItems = ConfirmationUseItems.GetComponent<Animator>();
        animPoPUp = PoPUpMessage.GetComponent<Animator>();
        localizedConfirmationGetItems = ConfirmationGetItems.GetComponentInChildren<LocalizedText>();
        localizedConfirmationUseItems = ConfirmationUseItems.GetComponentInChildren<LocalizedText>();
        localizedPopUp = PoPUpMessage.GetComponentInChildren<LocalizedText>();
    }

    public bool FindItemInInventory(string item)
    {
        return itemsForUse.Find(item);
    }

    public void ShowConfirmationScreen(string ItemName)
    {
        localizedConfirmationGetItems.UpdateText();
        ItemName += "?";
        localizedConfirmationGetItems.AddText(ItemName);
        animConfirmationGetItems.SetBool("AppearBox", true);
    }

    public void HideConfirmationScreen()
    {
        animConfirmationGetItems.SetBool("AppearBox", false);
    }

    public void ShowUseItemsScreen(string ItemName)
    {
        localizedConfirmationUseItems.UpdateText();
        ItemName += "?";
        localizedConfirmationUseItems.AddText(ItemName);
        animConfirmationUseItems.SetBool("AppearBox", true);
    }

    public void HideUseItemsScreen()
    {
        animConfirmationUseItems.SetBool("AppearBox", false);
    }

    public void GetItem()
    {
        if(collectableItem != null)
        {
            collectableItem.AddItem();
        }
    }

    public void UseItem()
    {
        if (interactableItem != null)
        {
            interactableItem.UseItem();
        }
    }

    public void ReceiveItemFromController(Interactable item)
    {
        try
        {
            interactableItem = item;
        }
        catch
        {
            interactableItem = null;
        }

        try
        {
            collectableItem = (CollectableItem)item;
        }
        catch
        {
            collectableItem = null;
        }
    }

    public bool AddItem(ItemObject item)
    {
        if (itemsForUse.isFull())
        {
            localizedPopUp.setNewKey("inventory_full");
            animPoPUp.SetTrigger("ShowBox");
            return false;
        }
        itemsForUse.AddItem(item);
        return true;
    }

    public bool AddDocument(ItemObject item)
    {
        documents.AddItem(item);
        return true;
    }

    public bool AddMemory(ItemObject item)
    {
        memories.AddItem(item);
        return true;
    }

    private void OnApplicationQuit()
    {
        itemsForUse.Clear();
        documents.Clear();
        memories.Clear();
    }
}
