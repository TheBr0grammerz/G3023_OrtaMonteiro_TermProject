using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<InventorySlot> items = new List<InventorySlot>();
    public bool isOpen = false;

    [SerializeField] InventoryUI _inventorySlotsUI, _shipSlotsUI;


    public void AddToInventory(IInventoryComponent item)
    {   
        
    }

    public void RemoveFromInventory(IInventoryComponent item)
    {

    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    private void OpenInventory()
    {
        if (_inventorySlotsUI == null)
        {
            if (!CreateUI())
            {
                Debug.LogError("Failed to create UI");
                return;
            }
        }



        isOpen = true;
        _inventorySlotsUI.transform.root.gameObject.SetActive(isOpen);

    }
    private void CloseInventory()
    {

        isOpen = false;
        _inventorySlotsUI.transform.root.gameObject.SetActive(isOpen);
    }

    private bool CreateUI()
    {
        var InvUI = Instantiate(Resources.Load<GameObject>("Inventory/Inventory UI"));

        foreach (var inventory in InvUI.GetComponentsInChildren<InventoryUI>())
        {
            if (inventory.name == "Inventory Slots")
            {
                _inventorySlotsUI = inventory;
            }
            else if (inventory.name == "Ship Slots")
            {
                _shipSlotsUI = inventory;
            }
        }

        return _inventorySlotsUI != null && _shipSlotsUI != null;

    }
}
