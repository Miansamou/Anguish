using System.Collections.Generic;
using UnityEngine;

public class DocumentsSlot : MonoBehaviour
{
    public PlayerMenu menu;

    private OtherUI[] slots;

    List<ItemObject> Documents = new List<ItemObject>();

    public PlayerItems inventory;

    public GameObject ImageLabel;

    private void Start()
    {
        inventory.getDocuments().onItemChangedCallback += UpdateDocumentsSlot;
    }

    private void OnEnable()
    {
        Documents = inventory.getDocuments().inventory;
        UpdateDocumentsSlot();
    }

    private void UpdateDocumentsSlot()
    {
        slots = transform.GetComponentsInChildren<OtherUI>();
        menu.disableImageLabel();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Documents.Count)
            {
                slots[i].AddItem(Documents[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
