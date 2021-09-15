using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles picking up items from the ground and podiums
// Also handles interating with levers
public class PickupItems : MonoBehaviour {

    public Transform playerPosition;
    public GameObject hintText;
    public Inventory inventory; 
    public LayerMask pickupLayer, interactLayer;
    public float grabDistance = 3.0f;
    public Transform HUD;  
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            playerPosition.localRotation = Camera.main.transform.localRotation;
            RaycastHit colliderHit;
            if (Physics.Raycast(playerPosition.position, playerPosition.forward, out colliderHit, grabDistance, pickupLayer)) {
                IInventoryItem item = colliderHit.collider.gameObject.GetComponent<IInventoryItem>();
                if(item!=null) {
                    inventory.addItem(item); 
                    hint(item.itemName);
                } else {
                    if(colliderHit.collider.gameObject.GetComponent<PodiumScript>() != null) {
                        item = colliderHit.collider.gameObject.GetComponent<PodiumScript>().item;
                        if (item != null) {
                            colliderHit.collider.gameObject.GetComponent<PodiumScript>().itemRemoved();
                            inventory.addItem(item); 
                            hint(item.itemName);
                        }
                    }
                }
            }
            if (Physics.Raycast(playerPosition.position, playerPosition.forward, out colliderHit, grabDistance, interactLayer)) {
                LeverScript lever = colliderHit.collider.gameObject.GetComponent<LeverScript>();
                if(lever != null) {
                    lever.Interact();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            int itemSlot = HUD.GetComponent<HUDScript>().selected;
            IInventoryItem[] itemList = HUD.GetComponent<HUDScript>().Items;
            if (itemList[itemSlot] != null) {
                IInventoryItem item = itemList[itemSlot];
                inventory.useItem(item);
            }
        }
    }

    // Sends out a message to the HUD
    public void hint (string name) {
        hintText.GetComponent<HintText>().setNewText("You got " + name);
    }
}
