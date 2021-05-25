using System.Collections.Generic;
using UnityEngine;

public class MemorySlot : MonoBehaviour
{
    public PlayerMenu menu;

    private OtherUI[] slots;

    List<ItemObject> Memories = new List<ItemObject>();

    public PlayerItems inventory;

    public GameObject ImageLabel;

    private void Start()
    {
        inventory.getMemories().onItemChangedCallback += UpdateMemoriesSlot;
    }

    private void OnEnable()
    {
        Memories = inventory.getMemories().inventory;
        UpdateMemoriesSlot();
    }

    private void UpdateMemoriesSlot()
    {
        slots = transform.GetComponentsInChildren<OtherUI>();
        menu.disableImageLabel();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Memories.Count)
            {
                slots[i].AddItem(Memories[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}

