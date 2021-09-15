using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is given to objects with 2 states
// active and destroy
// using an item will switch the item from active to destroyed
// this will only occur once per object
// Children of the object must be ordered correctly
// first children is the whole object and the second the destroyed version
public class DestroyableScript : MonoBehaviour {

    public GameObject KeyReference, sound;
    public Inventory inventory;
    public bool inRange = false;
    public bool destroyed = false;
    
    void Start() {
        inventory.ItemUsed += Inventory_ItemUsed;
    }

    void Inventory_ItemUsed(object sender, InventoryEventArgs e) {
        if ((e.item as MonoBehaviour).gameObject == KeyReference) {
            if (inRange && !destroyed) {
                sound.GetComponent<AudioSource>().Play();
                DestroyObject();
                destroyed = true;
            }
        }
    }

    void DestroyObject() { 
        Transform wholeObject = transform.GetChild(0);
        Transform destroyedObject = transform.GetChild(1);
        wholeObject.gameObject.SetActive(false);
        destroyedObject.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) {
        inRange = true;
    }

    private void OnTriggerExit(Collider other) {
        inRange = false;
    }
}
