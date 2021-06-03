using UnityEngine;
using TMPro;

public class TradeMemoriesButton : MonoBehaviour
{
    private ItemObject item;
    private string itemName;

    public TextMeshProUGUI buttonText;
    public GameObject ButtonObject;
    public ObeliskController controller;

    public void AddItem(ItemObject newItem)
    {
        item = newItem;
        itemName = LocalizedText.GetTextDeterminatedKey(item.nameItem);
        buttonText.text = itemName;
        ButtonObject.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;
        buttonText.text = "";
        ButtonObject.SetActive(false);
    }

    public void OnOfferItem()
    {
        if (item != null)
        {
            controller.AddToOfferedMemory(item);
        }
    }

    public void OnDontOfferItem()
    {
        if (item != null)
        {
            controller.AddToPlayerMemory(item);
        }
    }
}
