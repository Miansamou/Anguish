using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Memory Object", menuName = "InventorySystem/Items/Memory")]
public class MemoryObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Memory;
    }
}
