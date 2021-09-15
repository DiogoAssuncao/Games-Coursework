using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Podium can hold any of the status or the sandbag
// these can be removed into inventory and added back any number of times
public class PodiumScript : MonoBehaviour {
    public GameObject bearStatueReference, dragonStatueReference, horseStatueReference, sandBagReference;
    public GameObject correctStatue;
    public GameObject bearStatueStructure, dragonStatueStructure, horseStatueStructure, sandBagStructure;
    public GameObject activeStatue = null;
    public Inventory inventory;
    public IInventoryItem item = null;

    public bool inRange = false;
    public bool open = false;
    
    void Start() {
        inventory.ItemUsed += Inventory_ItemUsed;
        bearStatueStructure = transform.GetChild(0).gameObject;
        dragonStatueStructure = transform.GetChild(1).gameObject;
        horseStatueStructure = transform.GetChild(2).gameObject;
        sandBagStructure = transform.GetChild(3).gameObject;
    }

    void Inventory_ItemUsed(object sender, InventoryEventArgs e) {
        if ((e.item as MonoBehaviour).gameObject == bearStatueReference) {
            if (inRange && activeStatue == null) {
                bearStatueStructure.SetActive(true);
                item = e.item;
                inventory.removeItem(e.item);
                activeStatue = bearStatueReference;
                if(bearStatueReference == correctStatue){
                    open = true;
                }
            }
        } else if ((e.item as MonoBehaviour).gameObject == dragonStatueReference) {
            if (inRange && activeStatue == null) {
                dragonStatueStructure.SetActive(true); 
                item = e.item;
                inventory.removeItem(e.item);
                activeStatue = dragonStatueReference;
                if(dragonStatueReference == correctStatue){
                    open = true;
                }
            }
        } else if ((e.item as MonoBehaviour).gameObject == horseStatueReference) {
            if (inRange && activeStatue == null) {
                horseStatueStructure.SetActive(true); 
                item = e.item;
                inventory.removeItem(e.item);
                activeStatue = horseStatueReference;
                if(horseStatueReference == correctStatue){
                    open = true;
                }
            }
        } else if ((e.item as MonoBehaviour).gameObject == sandBagReference) {
            if (inRange && activeStatue == null) {
                sandBagStructure.SetActive(true); 
                item = e.item;
                inventory.removeItem(e.item);
                activeStatue = sandBagReference;
            }
        }
    }

    public void itemRemoved(){
        activeStatue = null;
        item = null;
        open = false;
        bearStatueStructure.SetActive(false);
        dragonStatueStructure.SetActive(false);
        horseStatueStructure.SetActive(false);
        sandBagStructure.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        inRange = true;
    }

    private void OnTriggerExit(Collider other) {
        inRange = false;
    }
}
