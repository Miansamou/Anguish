using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Usable,
    Document,
    Memory
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    public string nameItem;
}
