using UnityEngine;

public enum ItemType
{
    Usable,
    Document,
    Memory
}

public abstract class ItemObject : ScriptableObject
{
    public Sprite image;
    public ItemType type;
    public string nameItem;

    public int pages;
    public string[] keys;
}
