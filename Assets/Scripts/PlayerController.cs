using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player Controller
// Handles all player movement
public class PlayerController : MonoBehaviour {

    private Quaternion characterRotation;
    private Quaternion cameraRotation;
    public bool pushingObject = false;
    public float normalSpeed = 3.5f;    
    public float sprintSpeed = 6f;
    public float crouchSpeed = 1.5f;
    public float pushSpeed = 1.0f;
    public float jumpSpeed = 4f;
    public float crouchHeightScale = 0.6f;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public float xSensitivity = 3f;
    public float ySensitivity = 3f;
    public bool xInverted = false, yInverted = false, crouching = false;
    private float xRotation, yRotation, forwardMovement, lateralMovement, verticalSpeed;
    private bool noCollison = true;
    public bool locked = false;
    public GameObject escapeMenu, clock;
    private CollisionFlags CollisionFlag;

    void Start() {
        characterRotation = transform.localRotation;
        cameraRotation = Camera.main.transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update() {
        // Open Escape Menu
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (locked) {
                Continue();
            } else {
                locked = true;
                escapeMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                clock.GetComponent<ClockScript>().stop();
            }
        }

        if(!locked) {
            
            //Input
            forwardMovement = Input.GetAxis("Vertical");
            lateralMovement = Input.GetAxis("Horizontal");

            xRotation = Input.GetAxis("Mouse X");
            yRotation = Input.GetAxis("Mouse Y");

            if(Input.GetKey(KeyCode.LeftControl)){
                crouching = true;
            } else {
                if(noCollison){
                    crouching = false;
                }
            }


            //Crouch
            if(crouching){
                transform.localScale = new Vector3 (1f, crouchHeightScale, 1f);
                GetComponent<CapsuleCollider>().enabled = true;
            } else {
                transform.localScale = new Vector3 (1f, 1f, 1f);
                    GetComponent<CapsuleCollider>().enabled = false;
            }


            //Camera Movement
            float xRotationSpeed, yRotationSpeed;
            
            xRotationSpeed = xRotation * xSensitivity;
            if(xInverted) {
                xRotationSpeed *= -1; 
            }
            characterRotation = characterRotation * Quaternion.Euler(0f, xRotationSpeed, 0f);
            
            yRotationSpeed = -(yRotation * ySensitivity);
            if(yInverted) {
                yRotationSpeed *= -1;
            }
            cameraRotation = cameraRotation * Quaternion.Euler( yRotationSpeed, 0f, 0f);
            cameraRotation = ClampRotationAroundXAxis(cameraRotation);
            
            Camera.main.transform.localRotation = cameraRotation;
            transform.localRotation = characterRotation;
                    

            //Character Movement
            float forwardSpeed, lateralSpeed, movementSpeed;
            
            if(pushingObject){
                movementSpeed = pushSpeed;
            } else if (crouching){
                movementSpeed = crouchSpeed;
            } else if(Input.GetKey(KeyCode.LeftShift)){
                movementSpeed = sprintSpeed;
            } else {
                movementSpeed = normalSpeed;
            }
            
            forwardSpeed = movementSpeed * forwardMovement;
            lateralSpeed = movementSpeed * lateralMovement;

            verticalSpeed += Physics.gravity.y * Time.deltaTime;

            CharacterController characterController = GetComponent<CharacterController>();
            if (Input.GetButton("Jump") && characterController.isGrounded) {
                verticalSpeed = jumpSpeed;
            }

            Vector3 movementVector = new Vector3(lateralSpeed, verticalSpeed, forwardSpeed);
            movementVector = transform.rotation * movementVector;
            CollisionFlag = characterController.Move(movementVector * Time.deltaTime);
        }
    }

    public void Continue() {
        locked = false;
        escapeMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        clock.GetComponent<ClockScript>().start();
    }

    //Crouching Collision Triggers
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player"){
            noCollison = false; 
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag != "Player"){
            noCollison = false; 
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player"){
            noCollison = true; 
        }
    }

    // ClampRotationAroundXAxis Function 
    // From MouseLook.cs 
    // From Standart Assets
    Quaternion ClampRotationAroundXAxis(Quaternion q) {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

        angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    // OnControllerColliderHit Function
    // From FirstPersonController.cs
    // From Standart Assets
    // Adapted
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.gameObject.tag == "Plank" || hit.gameObject.tag == "ButtonCube"){
            return;
        }
        if (CollisionFlag != CollisionFlags.Below) {
            Rigidbody body = hit.collider.attachedRigidbody;
            if (body == null || body.isKinematic) {
                return;
            }
            body.AddForceAtPosition(GetComponent<CharacterController>().velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
        
}
