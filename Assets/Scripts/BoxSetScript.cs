using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Box Set in Chamber 3 
//Drops Gives an item (Planks) when all boxes are destroyed
//Inside one of the boxes is an item that will be activated when it is destroyed
public class BoxSetScript : MonoBehaviour {

    public GameObject insideObject;
    public GameObject boxContainsObject;
    public GameObject player;
    public GameObject planks;
    public Inventory inventory;
    private IInventoryItem item; 

    //Gets the plank IIventoryItem and disables the item withing the box
    void Start() {
        item = planks.GetComponent<IInventoryItem>();
        insideObject.GetComponent<SphereCollider>().enabled = false;
    }

    // Checks if all the children objects (must be Destroyable) are destroyed
    // If they are it gives the item to the player
    // If the box with the item inside is broken the item becomes avaiable to be picked up
    void Update() {
        bool allDestroyed = true;
        foreach (Transform child in transform){
            if (!child.gameObject.GetComponent<DestroyableScript>().destroyed){
                allDestroyed = false;
                break;
            }
        }
        if(allDestroyed) {
            GiveItem();
        }
        if(boxContainsObject.GetComponent<DestroyableScript>().destroyed){
            insideObject.GetComponent<SphereCollider>().enabled = true;
        }
    }

    // Gives the item (if there is one)
    void GiveItem() {
        if(item != null) {
            inventory.addItem(item);
            player.GetComponent<PickupItems>().hint(item.itemName);
            item = null;
        }
    }
}
