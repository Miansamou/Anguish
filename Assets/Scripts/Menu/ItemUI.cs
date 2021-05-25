using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    private ItemObject item;
    private string itemName;

    public PlayerMenu menu;
    public ItemsSlot adminItemSlot;
    public TextMeshProUGUI buttonText;
    public Button removeButton;
    public GameObject ImageLabel;
    public Image ItemPicture;
    public TextMeshProUGUI ItemPictureText;

    public void AddItem(ItemObject newItem)
    {
        item = newItem;
        itemName = LocalizedText.GetTextDeterminatedKey(item.nameItem);
        buttonText.text = itemName;
        removeButton.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;
        buttonText.text = "";
        removeButton.gameObject.SetActive(false);
    }

    public void OnClickItem()
    {
        menu.disableImageLabel();
        if (item != null)
        {
            ItemPicture.sprite = item.image;
            ItemPictureText.text = itemName;
            ImageLabel.SetActive(true);
        }
    }

    public void OnRemoveButton()
    {
        adminItemSlot.ShowConfirmationScreen(itemName, item);
    }
}
