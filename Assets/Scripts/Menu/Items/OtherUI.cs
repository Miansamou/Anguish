using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OtherUI : MonoBehaviour
{
    private ItemObject item;
    private string itemName;

    public PlayerMenu menu;
    public TextMeshProUGUI buttonText;
    public GameObject ButtonObject; 
    public GameObject ImageLabel;
    public Image ItemPicture;
    public TextMeshProUGUI ItemPictureText;
    public ManagerDescriptionItems ManagerDescriptopn;

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

    public void OnClickItem()
    {
        AudioManager.instance.Play("ClickButton");
        menu.disableImageLabel();
        if (item != null)
        {
            ItemPicture.sprite = item.image;
            ItemPictureText.text = itemName;
            ManagerDescriptopn.setDescription(item.pages, item.keys);
            ImageLabel.SetActive(true);
        }
    }
}
