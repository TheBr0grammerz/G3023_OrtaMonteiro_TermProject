using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIItem : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    [SerializeField] GameObject _Image;
    public static Canvas canvas;
    [SerializeField] GameObject hoveredObject;
    public bool hasItem = false;

    InventorySlot inventorySlot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_Image == null)
        {
            return;
        }
        _Image.transform.SetParent(canvas.transform);
        _Image.transform.SetAsLastSibling();
        _Image.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
            if (_Image == null)
        {
            return;
        }
        _Image.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_Image == null)
        {
            return;
        }
        _Image.transform.SetParent(transform);
        _Image.GetComponent<Image>().raycastTarget = false;
    }

        public void OnDrop(PointerEventData eventData)
    {
        GameObject heldItem = eventData.pointerDrag;
        if (heldItem == null )
        {
            return;
        }
        GameObject image = heldItem.GetComponent<InventoryUIItem>()._Image;
        heldItem.GetComponent<InventoryUIItem>()._Image = null;

        image.transform.SetParent(transform);
        _Image = image;
    }


    void Awake()
    {
        //image = transform.Find("Item").gameObject;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        hasItem = _Image;
        
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").GetComponentInParent<Canvas>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    internal void SetItem(InventorySlot inventorySlot)
    {
        this.inventorySlot = inventorySlot;
        
    }
}
