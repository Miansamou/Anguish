using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommomDoor : Interactable
{
    public GameObject textMessage;
    public Animator lockedMessage;
    public string key;
    public string animationName;
    public bool locked;
    public InventorySlot keyToOpen;
    public PlayerItems inventory;

    private LocalizedText localized;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Act(InputAction input)
    {

        LocalizedText localization = textMessage.GetComponent<LocalizedText>();
        localization.setNewKey(key);

        textMessage.SetActive(true);

        if (input.triggered)
        {
            Acting();
        }
    }

    public override void Acting()
    {
        if (locked)
        {
            if (inventory.FindItemInInventory(keyToOpen.item.nameItem))
            {
                inventory.ShowUseItemsScreen(LocalizedText.getTextDeterminatedKey(keyToOpen.item.nameItem));
                return;
            }
            
            localized = lockedMessage.gameObject.GetComponentInChildren<LocalizedText>();
            localized.setNewKey("locked");
            localized.UpdateText();
            lockedMessage.SetTrigger("ShowBox");
            return;
        }
        animator.SetTrigger(animationName);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public override void CancelAct()
    {
        textMessage.SetActive(false);
    }

    public override void UseItem()
    {
        locked = false;
        Acting();
    }
}
