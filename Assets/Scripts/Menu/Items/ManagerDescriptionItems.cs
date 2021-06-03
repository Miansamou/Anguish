using UnityEngine;

public class ManagerDescriptionItems : MonoBehaviour
{

    public GameObject Description;
    public LocalizedText[] texts;

    private int totalPages;
    private int currentPage;

    public void setDescription(int pages, string[] keys)
    {
        totalPages = pages;
        currentPage = 0;

        foreach(LocalizedText text in texts)
        {
            text.gameObject.SetActive(true);
        }

        for(int i = 0; i < totalPages; i++)
        {
            texts[i].SetNewKey(keys[i]);
        }

        foreach(LocalizedText text in texts)
        {
            text.gameObject.SetActive(false);
        }

        texts[0].gameObject.SetActive(true);

        Description.SetActive(true);
    }

    public void nextPage()
    {
        texts[currentPage].gameObject.SetActive(false);
        currentPage++;
        if (currentPage >= totalPages)
            currentPage = 0;
        texts[currentPage].gameObject.SetActive(true);
    }

    public void beforePage()
    {
        texts[currentPage].gameObject.SetActive(false);
        currentPage--;
        if (currentPage < 0)
            currentPage = totalPages - 1;
        texts[currentPage].gameObject.SetActive(true);
    }
}
