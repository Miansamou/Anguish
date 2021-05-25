using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName ="InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<ItemObject> inventory = new List<ItemObject>();
    public bool hasLimit;
    public int limit;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

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
        inventory.Add(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void Remove(ItemObject item)
    {
        inventory.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public bool Find(string item)
    {
        foreach(ItemObject slot in inventory)
        {
            if(slot.nameItem == item)
            {
                return true;
            }
        }
        return false;
    }
}
