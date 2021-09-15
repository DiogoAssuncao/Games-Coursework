using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Will handle the HUD 
// Adding and removing items
// Selecting and using 
public class HUDScript : MonoBehaviour {

    public Inventory inventory;
    private Transform[] Slots;
    public IInventoryItem[] Items;
    public int selected = 0;

    void Start() {
        inventory.ItemAdded += InventoryItemAdded;
        inventory.ItemRemoved += InventoryItemRemoved;
        Transform panel = transform.Find("InventorySlots");
        Slots = new Transform[8];
        Items = new IInventoryItem[8];
        int i = 0;
        foreach(Transform slot in panel) {
            Slots[i] = slot;
            if(i == selected){
                Transform sl = slot.Find("Selected");
                sl.GetComponent<Image>().enabled = true;
            }
            i++;
        }
    }

    private void InventoryItemAdded(object sender, InventoryEventArgs e) {
        int counter = 0;
        foreach(Transform slot in Slots) {
            Image image = slot.Find("Object").GetComponent<Image>();
            if (!image.enabled) {
                image.enabled = true;
                image.sprite = e.item.itemImage;
                Items[counter] = e.item;
                break;
            }
            counter++;
        }
    }

    private void InventoryItemRemoved(object sender, InventoryEventArgs e) {
        foreach(Transform slot in Slots) {
            Image image = slot.Find("Object").GetComponent<Image>();
            if (image.sprite == e.item.itemImage && image.enabled) {
                image.enabled = false; 
                break;
            }
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {select(0);}
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {select(1);}
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) {select(2);}
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) {select(3);}
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) {select(4);}
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) {select(5);}
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) {select(6);}
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) {select(7);}
    }

    void select(int slot){
        Slots[selected].Find("Selected").GetComponent<Image>().enabled = false;
        selected = slot;
        Slots[selected].Find("Selected").GetComponent<Image>().enabled = true;
    }
}
