using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public GameObject itemMenu;
    public GameObject documentMenu;
    public GameObject memoryMenu;
    public GameObject ImageLabel;
    public GameObject Description;

    public void ItemButton()
    {
        if (!itemMenu.activeInHierarchy)
        {
            itemMenu.SetActive(true);
            documentMenu.SetActive(false);
            memoryMenu.SetActive(false);
            disableImageLabel();
        }
    }

    public void documentButton()
    {
        if (!documentMenu.activeInHierarchy)
        {
            documentMenu.SetActive(true);
            itemMenu.SetActive(false);
            memoryMenu.SetActive(false);
            disableImageLabel();
        }
    }

    public void memoriesButton()
    {
        if (!memoryMenu.activeInHierarchy)
        {
            memoryMenu.SetActive(true);
            itemMenu.SetActive(false);
            documentMenu.SetActive(false);
            disableImageLabel();
        }
    }

    public void disableImageLabel()
    {
        Description.SetActive(false);
        ImageLabel.SetActive(false);
    }
}
