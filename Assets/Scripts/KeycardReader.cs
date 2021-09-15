using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Opens a door when an item is used in range
public class KeycardReader : MonoBehaviour {

    public GameObject KeyReference , swipe, beep;
    public Door doorObject;
    public Inventory inventory;
    public bool inRange = false;
    private bool open = false;

    void Start() {
        inventory.ItemUsed += Inventory_ItemUsed;
    }

    void Inventory_ItemUsed(object sender, InventoryEventArgs e) {
        if ((e.item as MonoBehaviour).gameObject == KeyReference) {
            if (inRange && !open) {
                if (swipe != null)
                    swipe.GetComponent<AudioSource>().Play();
                if (beep != null)
                    beep.GetComponent<AudioSource>().Play();
                doorObject.Open();
                open = true;
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
