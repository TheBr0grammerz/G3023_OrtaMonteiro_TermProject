using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<IInventoryComponent> items = new List<IInventoryComponent>();
    public bool isOpen = false;

    public Ship owner;

    [SerializeField] InventoryUI _inventorySlotsUI, _shipSlotsUI;


    public void AddToInventory(IInventoryComponent item)
    {   
        
    }

    public void RemoveFromInventory(IInventoryComponent item)
    {

    }

    void Awake()
    {
        owner = GetComponent<Ship>();
    }

    void Start()
    {
        CreateUI(false);
        _inventorySlotsUI.SetInventory(items);
        List<IInventoryComponent> shipItems = new List<IInventoryComponent>();
        foreach (var weapon in owner.weapons)
        {
            shipItems.Add(weapon.weaponInformation);
        }
        _shipSlotsUI.SetInventory(shipItems);
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

    public void OpenInventory()
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
    public void CloseInventory()
    {

        isOpen = false;
        _inventorySlotsUI.transform.root.gameObject.SetActive(isOpen);
    }

    private bool CreateUI(bool show = true)
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
        _inventorySlotsUI.transform.root.gameObject.SetActive(show);
        return _inventorySlotsUI != null && _shipSlotsUI != null;
    }
}
