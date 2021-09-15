using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This handles the safe oppening
// Using the right item will rotate the safe door
// it will also play a sound
public class SafeScript : MonoBehaviour {
    public GameObject KeyReference , sound;
    public Inventory inventory;
    public GameObject objectInside;
    public bool inRange = false;
    public float speed = 5f, waitTime = 4f;
    private bool open = false;
    
    void Start() {
        inventory.ItemUsed += Inventory_ItemUsed;
    }

    void Inventory_ItemUsed(object sender, InventoryEventArgs e) {
        if ((e.item as MonoBehaviour).gameObject == KeyReference) {
            if (inRange && !open) {
                open = true;
                objectInside.SetActive(true);
                inventory.removeItem(KeyReference.GetComponent<IInventoryItem>());
                sound.GetComponent<AudioSource>().Play();
            }
        }
    }

    void Update() { 
        if(open){
            if (waitTime > 0) {
                waitTime -= Time.deltaTime;
            } else{
                Transform safeDoor = transform.GetChild(1);
                if(safeDoor.rotation.y < -0.1f){
                    safeDoor.Rotate(Vector3.up * speed * Time.deltaTime);
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
