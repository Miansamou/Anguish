using UnityEngine;

public class CollectableItem : Interactable
{
    public GameObject textMessage;
    public string key;
    public ItemObject item;
    public PlayerItems inventory;
    private string nameItem;

    public override void Act()
    {
        LocalizedText localization = textMessage.GetComponent<LocalizedText>();
        localization.SetNewKey(key);

        nameItem = localization.GetTextKey();

        textMessage.SetActive(true);

        if (player.GetInteractTrigger())
        {
            Acting();
        }
    }

    public override void Acting()
    {
        inventory.ShowConfirmationScreen(nameItem);
    }

    public void AddItem()
    {
        switch (item.type)
        {
            case ItemType.Usable:
                if (inventory.AddItem(item))
                {
                    textMessage.SetActive(false);
                    Destroy(gameObject);
                }
                break;

            case ItemType.Document:
                if (inventory.AddDocument(item))
                {
                    textMessage.SetActive(false);
                    Destroy(gameObject);
                }
                break;
            case ItemType.Memory:
                if (inventory.AddMemory(item))
                {
                    textMessage.SetActive(false);
                    Destroy(gameObject);
                }
                break;
            default:
                Debug.Log("Item Type don't exist");
                break;
        }
    }

    public override void CancelAct()
    {
        textMessage.SetActive(false);
    }

    public override void UseItem()
    {
        Debug.Log("Unable to use item");
    }
}
