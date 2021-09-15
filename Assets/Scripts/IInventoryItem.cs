using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Items that can be inside the players inventory
public interface IInventoryItem {

    string itemName { get; }

    Sprite itemImage { get; }

    void onPickup();

}

public class InventoryEventArgs: EventArgs {
    
    public InventoryEventArgs(IInventoryItem item) {
        this.item = item;
    }
    
    public IInventoryItem item;
}
