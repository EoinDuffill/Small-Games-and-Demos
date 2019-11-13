using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    //List<IInventoryItem> items = new List<IInventoryItem>();
    IInventoryItem[] items = new IInventoryItem[3];

    public int slot = 0;
    public int noOfItems = 0;

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemUsed;
    public event EventHandler<SlotEventArgs> SlotUpdate;

    public void switchSlot(int s)
    {
        slot = s;
        for(int i = 0; i < items.Length; i++)
        {
            if(SlotUpdate != null)
            {
                SlotUpdate.Invoke(this, new SlotEventArgs(items[i], i==slot,i));
            }
        }
    }

    public void useSelectedItem()
    {
        if(items[slot] != null)
        {
            useItem(items[slot]);
        }
        
    }

    public void addItem(IInventoryItem item)
    {

        if(noOfItems < items.Length)
        {
            items[noOfItems] = item;
            noOfItems++;

            item.onPickup();
            // broadcast event to hud
            if (ItemAdded != null)
            {
                ItemAdded.Invoke(this, new InventoryEventArgs(item));
            }
        }
        
    }

    public void useItem(IInventoryItem item)
    {
        
        if(ItemUsed != null)
        {
            ItemUsed.Invoke(this, new InventoryEventArgs(item));
        }
    }
}
