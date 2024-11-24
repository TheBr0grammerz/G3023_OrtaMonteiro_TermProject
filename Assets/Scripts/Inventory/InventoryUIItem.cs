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

    IInventoryComponent inventorySlot;

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
        if (heldItem == null)
        {
            return;
        }
        GameObject image = heldItem.GetComponent<InventoryUIItem>()._Image;
        var hoveredComponenet = heldItem.GetComponent<InventoryUIItem>();

        if (hasItem) return;
        hoveredComponenet._Image = null;
        hoveredComponenet.hasItem = false;

        image.transform.SetParent(transform);
        _Image = image;
    }


    void Awake()
    {
       _Image = transform.Find("Item").gameObject;
       if (!hasItem)
       {
           _Image.SetActive(false);
       }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").GetComponentInParent<Canvas>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    internal void SetItem(IInventoryComponent inventorySlot)
    {
        this.inventorySlot = inventorySlot;
        _Image.GetComponent<Image>().sprite = inventorySlot.GetImage();
        _Image.GetComponent<Image>().preserveAspect = true;
        _Image.SetActive(true);
        hasItem = true;
    }
}
