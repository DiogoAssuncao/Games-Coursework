using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cleans the players Inventory after Chamber 2
// Requires a list of objects to be cleaned
public class CleanInventory : MonoBehaviour {

    public Inventory inventory;
    public List<GameObject> itemsCleaned;
    private LeverScript lever;
    private bool cleaned = false;

    void Start(){
        lever = transform.GetComponent<LeverScript>();
    }

    void Update() {
        bool open = lever.activated;
        if (open || !cleaned) {
            foreach (GameObject itemObject in itemsCleaned) {
                IInventoryItem item = itemObject.GetComponent<IInventoryItem>();
                inventory.removeItem(item);
            }
            cleaned = true;
        }
    }
}
