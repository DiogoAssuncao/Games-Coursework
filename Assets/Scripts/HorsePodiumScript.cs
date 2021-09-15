using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chamber 3 Horse Statue Puzzle
// Closes the door if nothing is on the podium
// Can be solved with the sandbag
public class HorsePodiumScript : MonoBehaviour {
    public GameObject horseStatue;
    public Door door;
    public PodiumScript podium;
    public bool open = false;

    void Start() {
        podium = transform.GetComponent<PodiumScript>();
        podium.horseStatueStructure.SetActive(true); 
        podium.item = horseStatue.GetComponent<IInventoryItem>();
        podium.activeStatue = podium.horseStatueReference;
    }
 
    void Update() {
        if(podium.activeStatue != null) {
            if(!open) {
                door.Open();
                open = true;
            }
        } else {
            if (open) {
                door.Close();
                open = false;
            }
        }
    }
}
