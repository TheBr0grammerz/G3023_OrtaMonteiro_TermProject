using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Inventory Item")]
public class NewBehaviourScript : ScriptableObject, IInventoryComponent
{

    [SerializeField] int itemID;
    [SerializeField] Sprite itemImage;
    [SerializeField] int maxStackSize;
    [SerializeField] int amount;
    [SerializeField] bool isStackable;
    [SerializeField] bool isUsable;

    public int GetAmount()
    {
        return amount;
    }

    public int GetID()
    {
        return itemID;
    }

    public Sprite GetImage()
    {
        return itemImage;
    }

    public int GetMaxStackSize()
    {
        return maxStackSize;
    }

}
