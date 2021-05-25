using System.Collections.Generic;
using UnityEngine;

public class TradeMemories : MonoBehaviour
{
    TradeMemoriesButton[] slots;

    public void UpdateMemories(List<ItemObject> memories)
    {
        if(slots == null)
        {
            slots = transform.GetComponentsInChildren<TradeMemoriesButton>();
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < memories.Count)
            {
                slots[i].AddItem(memories[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
