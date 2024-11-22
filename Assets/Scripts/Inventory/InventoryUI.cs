using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] InventoryUIItem[] inventoryUIItems;
    void Awake()
    {
        inventoryUIItems = GetComponentsInChildren<InventoryUIItem>();
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
