using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] InventoryUIItem[] inventoryUIItems;

    internal void SetInventory(List<InventorySlot> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            inventoryUIItems[i].SetItem(items[i]);
        }
    }

    void Awake()
    {
        inventoryUIItems = GetComponentsInChildren<InventoryUIItem>();
        InventoryUIItem.canvas = GetComponentInParent<Canvas>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventoryUIItems.Length; i++)
        {
            inventoryUIItems[i].name = $"Inventory Slot: {i+1}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
