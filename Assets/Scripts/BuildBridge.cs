using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Activates the bridge in chamber 3 on item use
public class BuildBridge : MonoBehaviour {
    public bool inRange = false, pickedUp = false;
    public GameObject planksReference, Bridge;
    public Inventory inventory;
    
    void Start() {
        inventory.ItemUsed += Inventory_ItemUsed;
    }

    void Inventory_ItemUsed(object sender, InventoryEventArgs e) {
        if (!pickedUp) {
            if ((e.item as MonoBehaviour).gameObject == planksReference) {
                if (inRange) {
                    Bridge.SetActive(true);
                    inventory.removeItem(e.item);
                    pickedUp = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        inRange = true;
    }

    private void OnTriggerExit(Collider other) {
        inRange = false;
    }

}
