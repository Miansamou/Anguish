using UnityEngine;

public class CommomDoor : Interactable
{
    public GameObject textMessage;
    public Animator lockedMessage;
    public string key;
    public string animationName;
    public bool locked;
    public ItemObject keyToOpen;
    public PlayerItems inventory;

    private LocalizedText localized;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Act()
    {

        LocalizedText localization = textMessage.GetComponent<LocalizedText>();
        localization.SetNewKey(key);

        textMessage.SetActive(true);

        if (player.getInteractTrigger())
        {
            Acting();
        }
    }

    public override void Acting()
    {
        if (locked)
        {
            if (inventory.FindItemInInventory(keyToOpen.nameItem))
            {
                inventory.ShowUseItemsScreen(LocalizedText.GetTextDeterminatedKey(keyToOpen.nameItem));
                return;
            }
            
            localized = lockedMessage.gameObject.GetComponentInChildren<LocalizedText>();
            localized.SetNewKey("locked");
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
