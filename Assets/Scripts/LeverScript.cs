using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flips a lever by rotating a moving structure (objects first child)
// Interactable with 'E' 
public class LeverScript : MonoBehaviour {
    public Door door;
    public GameObject sound;
    public float speed = 100f;
    public float maxRotation = -0.3f;
    public bool activated = false;
    
    void Update() {
       if(activated){
            Transform leverHandle = transform.GetChild(0);
            if(leverHandle.rotation.z > maxRotation){
                leverHandle.Rotate(Vector3.back * speed * Time.deltaTime);
            }
        } 
    }

    public void Interact () {
        if(!activated) {
            door.Open();
            activated = true;
            sound.GetComponent<AudioSource>().Play();
        } 
    }
}
