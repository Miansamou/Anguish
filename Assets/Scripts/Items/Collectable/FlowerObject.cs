using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flower Object", menuName = "InventorySystem/Items/Flower")]
public class FlowerObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Usable;
    }
}
