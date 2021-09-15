using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Inventory holds sends out events 
public class Inventory : MonoBehaviour {

    public List<IInventoryItem> items = new List<IInventoryItem>();

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> ItemUsed;

    public void addItem(IInventoryItem item) {
        items.Add(item);
        item.onPickup();
        if (ItemAdded != null) {
            ItemAdded.Invoke(this, new InventoryEventArgs(item));
        }
    }

    public void useItem(IInventoryItem item){
        if (ItemUsed != null) {
            ItemUsed.Invoke(this, new InventoryEventArgs(item));
        }
    }

    public void removeItem(IInventoryItem item){
        if (ItemRemoved != null) {
            ItemRemoved.Invoke(this, new InventoryEventArgs(item));
        }
    }

    
}
