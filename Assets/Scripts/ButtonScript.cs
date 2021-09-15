using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the Button Behaviour
public class ButtonScript : MonoBehaviour {
    public Door doorObject;
    public float holdToOpenTime = 0.3f;
    public float buttonSpeed = 1.5f;
    private Transform structure, upPosition, downPosition;
    private float elapsedTime;
    public bool open = false;
    private bool down = false;

    // Get the moveable part of the button (structure)
    // And the transforms for both possible positions
    void Start() {
        structure = transform.GetChild(1);
        upPosition = transform.GetChild(2);
        downPosition = transform.GetChild(3);
    }

    // Will update the position of the moveable structure according to the set position
    void FixedUpdate() {
        Transform target;
        if(down) {
            target = downPosition;
        } else {
            target = upPosition;
        }
        if(structure.localPosition != target.localPosition) {
            Vector3 velocity = GetVector(structure.localPosition, target.localPosition);
            structure.Translate(velocity * buttonSpeed * Time.deltaTime, Space.World);
        }
    }

    // Triggers Handle activating the button
    // Both a cube or the player may activate the button when standing on it
    // Button will deactivate when neither are on it
    // It will also wait for holdToOpenTime to activate 
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "ButtonCube" || other.tag == "Player") {
            elapsedTime = holdToOpenTime;
            down = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "ButtonCube" || other.tag == "Player") {
            if(elapsedTime > 0) {
                elapsedTime -= Time.deltaTime;
            } else if (!open) {
                doorObject.Open();
                open = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "ButtonCube" || other.tag == "Player") {
            if(open) {
                doorObject.Close();
                open = false;
                down = false;
            }
        }
    }

    // Used for calculation the vector between two positions
    private Vector3 GetVector(Vector3 from, Vector3 to){
        float x = to.x - from.x;
        float y = to.y - from.y;
        float z = to.z - from.z;
        return (new Vector3(x, y, z));
    }
}
