using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Push is very similar to carry objects
// Push doesnt allow vertical movement
// Will also slow you down
// Any Objects in the pushable layer may be pushed
public class PushObject : MonoBehaviour {
    public Transform playerPosition;
    public Transform holdPosition;
    public LayerMask layerMask;
    public float minDistance = 3f;
    public float maxDistance = 5.0f;
    public float speed = 5.0f;
    public float grabDistance = 5.0f;
    private PlayerController controller;
    private GameObject heldObject = null;
    private float distance;

    void Start(){
        controller = GetComponent<PlayerController>();
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.E)) {
            if (heldObject == null) {
                holdPosition.position = playerPosition.position;
                holdPosition.localRotation = Camera.main.transform.localRotation;
                RaycastHit colliderHit;
                if (Physics.Raycast(holdPosition.position, holdPosition.forward, out colliderHit, grabDistance, layerMask)) {
                    pickupObject(colliderHit);
                }
            } else {
                dropObject();
            }
        }
    }

    private void FixedUpdate() {
        if (heldObject != null && checkDistance()) {
            updateObjectPosition();
        }
    }

    private Vector3 GetVector(Vector3 from, Vector3 to){
        float x = to.x - from.x;
        float y = to.y - from.y;
        float z = to.z - from.z;
        return (new Vector3(x, y, z));
    }

    private void dropObject() {
        if (heldObject != null) {
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject.GetComponent<Rigidbody>().freezeRotation = false;
            heldObject.GetComponent<Rigidbody>().ResetInertiaTensor();
            heldObject = null;
            holdPosition.position = playerPosition.position;
            controller.pushingObject = false;   
        }
    }

    private void pickupObject(RaycastHit colliderHit) {
        heldObject = colliderHit.collider.gameObject;
        heldObject.GetComponent<Rigidbody>().useGravity = false;
        heldObject.GetComponent<Rigidbody>().freezeRotation = true;
        distance = colliderHit.distance;
        if (distance < minDistance){
            distance = minDistance;
        }
        holdPosition.Translate(new Vector3 (0 ,0, colliderHit.distance));
        controller.pushingObject = true;
    }

    private void updateObjectPosition(){
        holdPosition.position = playerPosition.position;
        holdPosition.Translate(new Vector3 (0 ,0, distance));
        Vector3 target = GetVector( heldObject.transform.localPosition, holdPosition.position);
        heldObject.GetComponent<Rigidbody>().velocity = target * speed;
    }

    private bool checkDistance(){
        if (heldObject != null) {
            if (Vector3.Distance(heldObject.transform.position, playerPosition.position) < maxDistance){
                return true;
            }
            dropObject();
        } 
        return false;
    }
}
