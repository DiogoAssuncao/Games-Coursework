using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handles the ability to carry an object
// Apart of the player
// this object should be in the correct layer and RigidBody set to Interpolate for smoothness
public class CarryObject : MonoBehaviour {
    public Transform playerPosition;
    public Transform holdPosition;
    public LayerMask layerMask;
    public float minDistance = 2.5f;
    public float maxDistance = 6.0f;
    public float speed = 5.0f;
    public float throwForce = 10.0f;
    public float grabDistance = 5.0f;
    private GameObject heldObject = null;
    private bool fire = false;
    private float distance;

    // Registers input (E key)
    // If any object is in range withing the pickupable Layer
    // This object will now be held
    // Unlike cubes, planks will folow the player rotation
    // This was done to give more control to the player, but removes smoothness from the carried object
    // Therefore only present in the planks where strictly necessary
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

        if (Input.GetButtonDown("Fire1")) {
            fire = true;   
        }
        if (heldObject != null && heldObject.tag == "Plank") {
            heldObject.transform.rotation = Quaternion.Euler(0.0f, playerPosition.rotation.eulerAngles.y, 0.0f);
        }
    }
    
    //Updates the object position or throws it (depending on input)
    private void FixedUpdate() {
        if (heldObject != null && checkDistance()) {
            updateObjectPosition();
            if(fire) {
                throwObject();
            }
        }
    }

    // Used to calculate the vectore between two positions 
    private Vector3 GetVector(Vector3 from, Vector3 to){
        float x = to.x - from.x;
        float y = to.y - from.y;
        float z = to.z - from.z;
        return (new Vector3(x, y, z));
    }

    // resets object properties and lets go of the object
    private void dropObject() {
        if (heldObject != null) {
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            if(heldObject.tag != "Plank"){
                heldObject.GetComponent<Rigidbody>().freezeRotation = false;
            }
            heldObject.GetComponent<Rigidbody>().ResetInertiaTensor();
            heldObject = null;
            holdPosition.position = playerPosition.position;   
        }
    }

    // Picks up the object
    // Removes gravity
    // Freezes rotation (not on planks)
    // checks if distance is within parameters
    // Range to the object will kept and tryed to maintained when carrying it (if it is withing the allowed distance) 
    private void pickupObject(RaycastHit colliderHit) {
        heldObject = colliderHit.collider.gameObject;
        heldObject.GetComponent<Rigidbody>().useGravity = false;
        if(heldObject.tag != "Plank"){
                heldObject.GetComponent<Rigidbody>().freezeRotation = true;
            }
        distance = colliderHit.distance;
        if (distance < minDistance){
            distance = minDistance;
        }
        holdPosition.Translate(new Vector3 (0 ,0, colliderHit.distance));
    }

    // Position of the object is maintained by having 2 transforms on the player
    // the first playerPosition will be a set mark, will always have the same location and rotation as the player
    // holdPosition will calculate the position of the held object
    // by setting its position as the playerPosition position
    // getting the cameras rotation (object will follow the camera/mouse)
    // and moving foward by the calculated distance (keeping the object as far as we want it)
    // we then get a velocity vector between the object and this transform
    // and apply the velocity to the object 
    // (increases smoothness as oposed to changing its position will also not allow the object to be pushed through walls)
    // like this we can keep the object at a certain distance and control it with both the mouse and keys
    // giving the player extra control over the object
    private void updateObjectPosition(){
        holdPosition.position = playerPosition.position;
        holdPosition.localRotation = Camera.main.transform.localRotation;
        holdPosition.Translate(new Vector3 (0 ,0, distance + heldObject.GetComponent<BoxCollider>().bounds.size.z / 2));
        Vector3 target = GetVector( heldObject.transform.localPosition, holdPosition.position);
        heldObject.GetComponent<Rigidbody>().velocity = target * speed;
    }
    
    //Applies a force and drops the object to throw it
    private void throwObject(){
        fire = false;
        heldObject.GetComponent<Rigidbody>().AddForce(holdPosition.forward * throwForce, ForceMode.Impulse);
        dropObject();
    }

    // checks if the distance is withing the input parameters
    // if not (it gets too far away) it will drop the object
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
