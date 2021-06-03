using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject itemMenu;
    [SerializeField]
    private GameObject documentMenu;
    [SerializeField]
    private GameObject memoryMenu;
    [SerializeField]
    private GameObject imageLabel;
    [SerializeField]
    private GameObject description;

    public void ItemButton()
    {
        AudioManager.instance.Play("ClickButton");
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
        AudioManager.instance.Play("ClickButton");
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
        AudioManager.instance.Play("ClickButton");
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
        description.SetActive(false);
        imageLabel.SetActive(false);
    }
}
