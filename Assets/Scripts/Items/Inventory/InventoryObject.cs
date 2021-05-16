using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName ="InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventory = new List<InventorySlot>();
    public bool hasLimit;
    public int limit;

    public void Clear()
    {
        inventory.Clear();
    }

    public bool isFull()
    {
        if (!hasLimit)
            return false;
        if (inventory.Count >= limit)
            return true;
        return false;
    }

    public void AddItem(ItemObject item)
    {
        inventory.Add(new InventorySlot(item));
    }

    public bool Find(string item)
    {
        foreach(InventorySlot slot in inventory)
        {
            if(slot.item.nameItem == item)
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;

    public InventorySlot(ItemObject item)
    {
        this.item = item;
    }
}