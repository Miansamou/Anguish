using System.Collections.Generic;
using UnityEngine;

public class ItemsSlot : MonoBehaviour
{
    public PlayerMenu menu;

    private ItemUI[] slots;

    List<ItemObject> Items = new List<ItemObject>();

    public PlayerItems inventory;

    public GameObject ImageLabel;

    public GameObject ConfirmationRemoveItems;

    private ItemObject auxItem;

    private Animator animConfirmationRemoveItems;
    private LocalizedText localizedConfirmationRemoveItems;

    private void Start()
    {
        inventory.getItems().onItemChangedCallback += UpdateItemsSlot;
        animConfirmationRemoveItems = ConfirmationRemoveItems.GetComponent<Animator>();
        localizedConfirmationRemoveItems = ConfirmationRemoveItems.GetComponentInChildren<LocalizedText>();
    }

    private void OnEnable()
    {
        Items = inventory.getItems().inventory;
        UpdateItemsSlot();
    }

    public void ShowConfirmationScreen(string ItemName, ItemObject item)
    {
        localizedConfirmationRemoveItems.UpdateText();
        ItemName += "?";
        localizedConfirmationRemoveItems.AddText(ItemName);
        animConfirmationRemoveItems.SetBool("AppearBox", true);
        auxItem = item;
    }

    public void HideConfirmationScreen()
    {
        animConfirmationRemoveItems.SetBool("AppearBox", false);
        auxItem = null;
    }

    public void RemoveItemScreen()
    {
        RemoveItem(auxItem);
        animConfirmationRemoveItems.SetBool("AppearBox", false);
        auxItem = null;
    }

    private void UpdateItemsSlot()
    {
        slots = transform.GetComponentsInChildren<ItemUI>();
        menu.disableImageLabel();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Items.Count)
            {
                slots[i].AddItem(Items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void RemoveItem(ItemObject item)
    {
        inventory.RemoveItem(item);
        UpdateItemsSlot();
    }
}
