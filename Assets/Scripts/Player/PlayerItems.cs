using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    #region variables

    [Header("Inventories")]
    [SerializeField]
    private InventoryObject itemsForUse;
    [SerializeField]
    private InventoryObject documents;
    [SerializeField]
    private InventoryObject memories;

    [Header("Messages")]
    [SerializeField]
    private GameObject confirmationGetItems;
    [SerializeField]
    private GameObject confirmationUseItems;
    [SerializeField]
    private GameObject poPUpMessage;

    private Animator animConfirmationGetItems;
    private Animator animConfirmationUseItems;
    private Animator animPoPUp;
    private LocalizedText localizedConfirmationGetItems;
    private LocalizedText localizedConfirmationUseItems;
    private LocalizedText localizedPopUp;
    private CollectableItem collectableItem;
    private IInteractable interactableItem;
    private bool receiveFirstItem;

    #endregion

    #region initialization

    private void Start()
    {
        receiveFirstItem = false;
        animConfirmationGetItems = confirmationGetItems.GetComponent<Animator>();
        animConfirmationUseItems = confirmationUseItems.GetComponent<Animator>();
        animPoPUp = poPUpMessage.GetComponent<Animator>();
        localizedConfirmationGetItems = confirmationGetItems.GetComponentInChildren<LocalizedText>();
        localizedConfirmationUseItems = confirmationUseItems.GetComponentInChildren<LocalizedText>();
        localizedPopUp = poPUpMessage.GetComponentInChildren<LocalizedText>();
    }

    #endregion

    #region screens

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

    #endregion

    #region usable item

    public bool FindItemInInventory(string item)
    {
        return itemsForUse.Find(item);
    }

    public void UseItem()
    {
        if (interactableItem != null)
        {
            interactableItem.UseItem();
        }
    }

    public void ReceiveItemFromController(IInteractable item)
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

    #endregion

    #region add/remove

    public bool AddItem(ItemObject item, string itemMessage)
    {
        if (!receiveFirstItem)
        {
            receiveFirstItem = true;
            StartCoroutine(TutorialBoxController.instance.ActiveTextTutorial("Inventory", 0f));
            StartCoroutine(TutorialBoxController.instance.PlayAnimation(0.75f));
            StartCoroutine(TutorialBoxController.instance.ExitTutorial(3f));
        }

        if (itemsForUse.isFull())
        {
            localizedPopUp.SetNewKey("inventory_full");
            localizedPopUp.UpdateText();
            animPoPUp.SetTrigger("ShowBox");
            return false;
        }
        localizedPopUp.SetNewKey(itemMessage);
        localizedPopUp.UpdateText();
        animPoPUp.SetTrigger("ShowBox");
        itemsForUse.AddItem(item);
        return true;
    }

    public void RemoveItem(ItemObject item)
    {
        itemsForUse.Remove(item);
    }

    public bool AddDocument(ItemObject item, string itemMessage)
    {
        if (!receiveFirstItem)
        {
            receiveFirstItem = true;
            StartCoroutine(TutorialBoxController.instance.ActiveTextTutorial("Inventory", 0f));
            Invoke(nameof(TutorialBoxController.instance.PlayAnimation), 0.75f);
            Invoke(nameof(TutorialBoxController.instance.ExitTutorial), 3f);
        }

        localizedPopUp.SetNewKey(itemMessage);
        localizedPopUp.UpdateText();
        animPoPUp.SetTrigger("ShowBox");
        documents.AddItem(item);
        return true;
    }

    public bool AddMemory(ItemObject item, string itemMessage)
    {
        localizedPopUp.SetNewKey(itemMessage);
        localizedPopUp.UpdateText();
        animPoPUp.SetTrigger("ShowBox");
        memories.AddItem(item);
        return true;
    }

    #endregion

    #region get/set

    public void GetItem()
    {
        if (collectableItem != null)
        {
            collectableItem.AddItem();
        }
    }

    public InventoryObject GetItems()
    {
        return itemsForUse;
    }

    public InventoryObject GetDocuments()
    {
        return documents;
    }

    public InventoryObject GetMemories()
    {
        return memories;
    }

    #endregion

    #region end application

    private void OnApplicationQuit()
    {
        itemsForUse.Clear();
        documents.Clear();
        memories.Clear();
    }

    #endregion
}
