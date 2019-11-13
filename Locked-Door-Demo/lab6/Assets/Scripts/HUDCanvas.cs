using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCanvas : MonoBehaviour {

    public Inventory inventory;

    public event EventHandler<InventoryEventArgs> ItemAdded;

    // Use this for initialization
    void Start () {
        inventory.ItemAdded += InventoryItemAdded;
        inventory.SlotUpdate += SlotUpdate;
    }

    private void SlotUpdate(object sender, SlotEventArgs e)
    {
        Transform slot = transform.Find("InventoryHUD").GetChild(e.slot);
        Image slotHeader = slot.GetChild(0).GetComponent<Image>();
        
        if (e.selected)
        {
            slotHeader.color = new Color(slotHeader.color.r, slotHeader.color.g, slotHeader.color.b, 1);
            Debug.Log(slotHeader.name);
        }
        else
        {
            slotHeader.color = new Color(slotHeader.color.r, slotHeader.color.g, slotHeader.color.b, 0);
            Debug.Log(slotHeader.name);
        }
    }

    private void InventoryItemAdded(object sender, InventoryEventArgs e)
    {
        Transform panel = transform.Find("InventoryHUD");

        foreach (Transform slot in panel)
        {
            Image image = slot.GetComponent<Image>();
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.item.itemImage;
                break;
            }
        }
    }
}
