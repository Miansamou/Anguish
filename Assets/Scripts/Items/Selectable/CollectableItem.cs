using UnityEngine;

public class CollectableItem : IInteractable
{
    #region variables

    [SerializeField]
    private string key;
    [SerializeField]
    private string popUpMessage;
    [SerializeField]
    private ItemObject item;
    [SerializeField]
    private PlayerItems inventory;

    private string nameItem;

    #endregion

    #region initialization

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    #endregion

    #region actions

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
                if (inventory.AddItem(item, popUpMessage))
                {
                    textMessage.SetActive(false);
                    Destroy(gameObject);
                }
                break;

            case ItemType.Document:
                if (inventory.AddDocument(item, popUpMessage))
                {
                    textMessage.SetActive(false);
                    Destroy(gameObject);
                }
                break;
            case ItemType.Memory:
                if (inventory.AddMemory(item, popUpMessage))
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

    #endregion
}
