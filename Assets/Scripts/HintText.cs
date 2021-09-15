using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Will show some text for a set to time in the player HUD
// Used for hints and object pickups
public class HintText : MonoBehaviour {
    
    public GameObject hintText;
    public string text;
    public float timeOnScreen = 5f;
    private bool seen = false;
    private float elapsedTime = 0;

    void Update() {
        if (elapsedTime > 0) {
            elapsedTime -= Time.deltaTime;
            if (elapsedTime <= 0) {
                hintText.GetComponent<TextMeshProUGUI>().text = "";
            }
        } 
    }

    // Hints use triggers
    void OnTriggerEnter(Collider other) {
        if (!seen && other.gameObject.tag == "Player") {
            seen = true;
            hintText.GetComponent<TextMeshProUGUI>().text = text;
            elapsedTime = timeOnScreen;
        }
    }

    // picked up items will call for new text
    public void setNewText(string name) {
        hintText.GetComponent<TextMeshProUGUI>().text = name;
        elapsedTime = timeOnScreen;
    }
}
